using FluentValidation;
using MargoFetcher.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MargoFetcher.Domain.Validators
{
    public class ItemValidator : AbstractValidator<Item>
    {
        public ItemValidator()
        {
            RuleFor(x => x.userId).GreaterThan(1);
            RuleFor(x => x.charId).GreaterThan(1);
            RuleFor(x => x.hid).NotNull();
            RuleFor(x => x.name).NotNull();
            RuleFor(x => x.icon).NotNull();
            RuleFor(x => x.st).NotNull();
            RuleFor(x => x.stat).NotNull();
            RuleFor(x => x.tpl).NotNull();
        }
    }
}
