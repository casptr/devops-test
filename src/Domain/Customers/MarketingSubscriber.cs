using Ardalis.GuardClauses;
using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Customers
{
    public class MarketingSubscriber : Entity
    {
        public EmailAddress Email { get;  } = default!;

        private MarketingSubscriber() { }

        public MarketingSubscriber(EmailAddress email)
        {
            Email = Guard.Against.Null(email, nameof(Email));
        }

    }
}
