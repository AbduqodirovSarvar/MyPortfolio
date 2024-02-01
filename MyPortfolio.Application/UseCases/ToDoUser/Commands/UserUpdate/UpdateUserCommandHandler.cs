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

namespace MyPortfolio.Application.UseCases.ToDoUser.Commands.UserUpdate
{
    public sealed class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserViewModel>
    {
        private readonly IAppDbContext _context;
        private readonly ICurrentUserService _currentUser;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateUserCommandHandler> _logger;
        private readonly IFileService _fileService;
        private readonly IHashService _hashService;
        public UpdateUserCommandHandler(
            IAppDbContext context,
            ICurrentUserService currentUser,
            IMapper mapper,
            ILogger<UpdateUserCommandHandler> logger,
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

        async Task<UserViewModel> IRequestHandler<UpdateUserCommand, UserViewModel>.Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                                     .FirstOrDefaultAsync(x => x.Id == request.Id && x.Id == _currentUser.UserId, cancellationToken)
                                     ?? throw new NotFoundException("User not found");

            var changedUser = new User(
                                    request.FirstName ?? user.FirstName,
                                    request.LastName ?? user.LastName,
                                    request.MiddleName ?? user.MiddleName,
                                    request.Email ?? user.Email,
                                    request.Password != null ? _hashService.GetHash(request.Password) : user.Password,
                                    request.BirthDay ?? user.BirthDay,
                                    request.Gender != null ? (Gender)Enum.Parse(typeof(Gender), request.Gender) : user.Gender,
                                    request.Profession ?? user.Profession,
                                    request.AboutMe ?? user.AboutMe,
                                    request.PhoneNumber ?? user.PhoneNumber,
                                    request.Photo != null ? (await _fileService.SaveFileAsync(request.Photo))?.ToString() : user.PhotoUrl,
                                    request.Resume != null ? (await _fileService.SaveFileAsync(request.Resume))!.ToString() : user.ResumeUrl
                                    );

            user.Change(changedUser);

            bool result = (await _context.SaveChangesAsync(cancellationToken)) > 0;

            string resultMessage = result ? "User (ID: {deletedUserId}) updated by user (ID: {UserId})"
                                      : "User (ID: {deletedUserId}) couldn't update by user (ID: {UserId})";

            _logger.LogInformation(resultMessage, user.Id, _currentUser.UserId);

            return _mapper.Map<UserViewModel>(user);
        }
    }
}
