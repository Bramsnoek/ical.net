﻿using System;
using System.Collections.Generic;
using System.Text;
using DDay.iCal.Components;

namespace DDay.iCal.Validator.RFC2445
{
	public class EventOrganizerPropertyValidator :
        IValidator
	{
        #region Public Properties

        public iCalendar iCalendar { get; set; }

        #endregion

        #region Constructors

        public EventOrganizerPropertyValidator(iCalendar cal)
        {
            iCalendar = cal;
        }

        #endregion

        #region IValidator Members

        public IValidationResult[] Validate()
        {
            ValidationResult result = new ValidationResult("eventOrganizerProperty");
            result.Passed = true;

            List<IValidationError> errors = new List<IValidationError>();

            foreach (Event evt in iCalendar.Events)
            {
                ValidationResult evtResult = ValidationResult.GetCompositeResults(
                    "eventOrganizerProperty",
                    new PropertyCountValidator(evt, "VEVENT", "ORGANIZER")                    
                );

                if (!evtResult.Passed.Value)
                {
                    result.Passed = false;
                    errors.AddRange(evtResult.Errors);
                }
            }

            result.Errors = errors.ToArray();
            return new IValidationResult[] { result };
        }

        #endregion
	}
}