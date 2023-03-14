using FluentValidation;
using MargonemPlayerFetcher.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MargonemPlayerFetcher.Domain.Validators
{
    public class PlayerValidator : AbstractValidator<Player>
    {
        public PlayerValidator()
        {
            RuleFor(x => x.userId).NotNull();
            RuleFor(x => x.charId).NotNull();
            RuleFor(x => x.nick).NotNull();
            RuleFor(x => x.server).NotNull();
            RuleFor(x => x.profession).NotNull();
            RuleFor(x => x.rank).NotNull();
            RuleFor(x => x.level).NotNull();
        }
    }
}
