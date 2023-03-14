using FluentValidation;
using MargonemPlayerFetcher.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MargonemPlayerFetcher.Domain.Validators
{
    public class ItemValidator : AbstractValidator<Item>
    {
        public ItemValidator()
        {
            RuleFor(x => x.userId).NotNull();
            RuleFor(x => x.charId).NotNull();
            RuleFor(x => x.hid).NotNull();
            RuleFor(x => x.name).NotNull();
            RuleFor(x => x.icon).NotNull();
            RuleFor(x => x.st).NotNull();
            RuleFor(x => x.stat).NotNull();
            RuleFor(x => x.tpl).NotNull();
        }
    }
}
