﻿using MediatR;
using MyPortfolio.Entity.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortfolio.Entity.Enums;
using MyPortfolio.Application.Models.ViewModels;

namespace MyPortfolio.Application.UseCases.ToDoUser.Commands.LanguageCreate
{
    public sealed class CreateLanguageCommand : IRequest<UserLanguageViewModel>
    {
        public string Name { get; set; } = null!;
        public string LanguageLevel { get; set; } = null!;
    }
}
