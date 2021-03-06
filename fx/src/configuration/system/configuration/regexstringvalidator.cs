//------------------------------------------------------------------------------
// <copyright file="RegexStringValidator.cs" company="Microsoft">
//     
//      Copyright (c) 2006 Microsoft Corporation.  All rights reserved.
//     
//      The use and distribution terms for this software are contained in the file
//      named license.txt, which can be found in the root of this distribution.
//      By using this software in any fashion, you are agreeing to be bound by the
//      terms of this license.
//     
//      You must not remove this notice, or any other, from this software.
//     
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Security.Permissions;
using System.Xml;
using System.Collections.Specialized;
using System.Globalization;
using System.ComponentModel;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;

namespace System.Configuration {

    public class RegexStringValidator : ConfigurationValidatorBase {
        private string _expression;
        private Regex _regex;

        public RegexStringValidator(string regex) {
            if (string.IsNullOrEmpty(regex)) {
                throw ExceptionUtil.ParameterNullOrEmpty("regex");
            }

            _expression = regex;
            _regex = new Regex(regex, RegexOptions.Compiled);
        }
        
        public override bool CanValidate(Type type) {
            return (type == typeof(string));
        }

        public override void Validate(object value) {
            ValidatorUtils.HelperParamValidation(value, typeof(string));

            if (value == null) {
                return;
            }

            Match match = _regex.Match((string)value);

            if (!match.Success) {
                throw new ArgumentException(SR.GetString(SR.Regex_validator_error, _expression));
            }
        }
    }
}
