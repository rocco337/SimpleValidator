using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleValidator
{
    public class ObjectValidatorBase<T>
    {
        /// <summary>
        /// model that is validated
        /// </summary>
        protected T Model;

        /// <summary>
        /// Check if model meetf all rules
        /// </summary>
        public bool IsValid()
        {
            //select all ruiles without OnlyIf expression or with satisfied OnlyIf expression
            var invalidResults = _rules.Where(m => m.OnlyIf == null || (m.OnlyIf != null && m.OnlyIf(Model)));

            //select all rules that arent satisfied
            invalidResults = invalidResults.Where(m => !m.ValidationExpression(Model));

            //set error messages of unsatisfied rules
            ErrorMessages = invalidResults.Select(m => m.ErrorMessage).ToList();
            
            return !invalidResults.Any();
        }

        /// <summary>
        /// Get error messages for rules that are not satisfied
        /// </summary>
        public List<string> ErrorMessages { get; set; }

        /// <summary>
        /// List of rules that are specified for model
        /// </summary>
        private readonly List<ValidationRule<T>> _rules = new List<ValidationRule<T>>();
        
        protected void AddRule(Func<T, bool> validationExpression, Func<T, bool> onlyIf, String errorMessage)
        {
            _rules.Add(new ValidationRule<T>()
            {
                ValidationExpression = validationExpression,
                OnlyIf = onlyIf,
                ErrorMessage = errorMessage
            });
        }

        protected void AddRule(Func<T, bool> validationExpression, String errorMessage)
        {
            AddRule(validationExpression, null, errorMessage);
        }

        private class ValidationRule<T>
        {
            /// <summary>
            /// Validation rule expression, examp. check is string null or empty !string.IsNullOrEmpty(o.Name)
            /// </summary>
            public Func<T, bool> ValidationExpression { get; set; }

            /// <summary>
            /// You can specify OnlyIf expression, if this expresion is specified than ValidationExpression is going to be evaluated only if OnlyIf rule is satisfied
            /// </summary>
            public Func<T, bool> OnlyIf { get; set; }
       
            /// <summary>
            /// Specify error message that will be returned if rule is not satisifed
            /// </summary>
            public string ErrorMessage { get; set; }
        }
    }
}
