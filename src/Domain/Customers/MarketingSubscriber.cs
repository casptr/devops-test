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

        public MarketingSubscriber(EmailAddress email)
        {
            Email = Guard.Against.Null(email, nameof(Email));
        }

    }
}
