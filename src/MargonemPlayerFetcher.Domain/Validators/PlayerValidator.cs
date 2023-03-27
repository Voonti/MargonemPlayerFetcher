using FluentValidation;
using MargoFetcher.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MargoFetcher.Domain.Validators
{
    public class PlayerValidator : AbstractValidator<Player>
    {
        public PlayerValidator()
        {
            RuleFor(x => x.userId).GreaterThan(1);
            RuleFor(x => x.charId).GreaterThan(1);
            RuleFor(x => x.nick).NotNull();
            RuleFor(x => x.server).NotNull();
            RuleFor(x => x.profession).NotNull();
            RuleFor(x => x.rank).NotNull();
            RuleFor(x => x.level).NotNull().LessThan(33);
        }
    }
}
