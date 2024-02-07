using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyPortfolio.Application.Abstractions.Interfaces;
using MyPortfolio.Application.Models.ViewModels;
using MyPortfolio.Entity.Entities;
using MyPortfolio.Entity.Enums;
using MyPortfolio.Entity.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.UseCases.ToDoUser.Commands.UserCreate
{
    public sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserViewModel>
    {
        private readonly IAppDbContext _context;
        private readonly ICurrentUserService _currentUser;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateUserCommand> _logger;
        private readonly IFileService _fileService;
        private readonly IHashService _hashService; 
        public CreateUserCommandHandler(
            IAppDbContext context,
            ICurrentUserService currentUser,
            IMapper mapper,
            ILogger<CreateUserCommand> logger,
            IFileService fileService,
            IHashService hashService)
        {
            _context = context;
            _currentUser = currentUser;
            _mapper = mapper;
            _logger = logger;
            _fileService = fileService;
            _hashService = hashService;
        }

        async Task<UserViewModel> IRequestHandler<CreateUserCommand, UserViewModel>.Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                                     .FirstOrDefaultAsync(x => x.PhoneNumber == request.PhoneNumber || x.Email == request.Email, cancellationToken)
                                      ?? new User(
                                                    request.FirstName,
                                                    request.LastName,
                                                    request.MiddleName,
                                                    request.Email,
                                                    _hashService.GetHash(request.Password),
                                                    request.BirthDay,
                                                    (Gender)Enum.Parse(typeof(Gender), request.Gender),
                                                    request.Profession,
                                                    request.AboutMe,
                                                    request.PhoneNumber,
                                                    (await _fileService.SaveFileAsync(request.Photo))?.ToString(),
                                                    (await _fileService.SaveFileAsync(request.Resume))!.ToString()
                                                    );

            await _context.Users.AddAsync(user, cancellationToken);

            string resultMessage = (await _context.SaveChangesAsync(cancellationToken)) > 0
                                          ? "User (ID: {userId}) created)"
                                          : "User (ID: {userId}) couldn't create";

            _logger.LogInformation(resultMessage, user.Id, _currentUser.UserId);

            return _mapper.Map<UserViewModel>(user);
        }
    }
}
