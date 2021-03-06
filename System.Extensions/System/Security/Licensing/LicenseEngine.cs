using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;

namespace System.Security.Licensing {


    public class LicenseEngine<TCompiled> : DisplayClass 
        where TCompiled : DisplayRecord
        {

        public ImmutableList<TCompiled> LicenseCodes { get; private set; } = ImmutableList<TCompiled>.Empty;
        protected ImmutableList<LicenseParserBase<TCompiled>> Parsers { get; }

        public override DisplayBuilder GetDebuggerDisplayBuilder(DisplayBuilder Builder) {
            return base.GetDebuggerDisplayBuilder(Builder)
                .Data.AddCount(LicenseCodes)
                .Data.AddCount(Parsers)
                ;
        }

        public LicenseEngine()
        {
            this.Parsers = InitializeLicenseParsers()
                .ToImmutableList()
                ;

            this.LicenseCodes = InitializeDefaultLicenses()
                .ToImmutableList()
                ;

        }

        protected virtual IEnumerable<TCompiled> InitializeDefaultLicenses() {
            yield break;
        }

        protected virtual IEnumerable<LicenseParserBase<TCompiled>> InitializeLicenseParsers()
        {
            yield return LicenseParsers.Default<TCompiled>();
        }


        private void AddLicense(TCompiled? License) {
            if(License is { } V1 && !LicenseCodes.Contains(V1)) {
                LicenseCodes = LicenseCodes.Add(V1);
            }
        }

        private void RemoveLicense(TCompiled? License) {
            if (License is { }) {
                LicenseCodes = LicenseCodes.Remove(License);
            }
        }


        public TCompiled Load(string Key) {
            var ret = Parse(Key);

            var Errors = Validate(ret).ToList();
            
            if(Errors.Count > 0)
            {
                throw new AggregateException(Errors);
            }
            
            AddLicense(ret);

            return ret;
        }


        public bool TryLoad(string LicenseText, [NotNullWhen(true)] out TCompiled? License)
        {
            return TryLoad(LicenseText, out License, out _);
        }

        public bool TryLoad(string LicenseText, [NotNullWhen(true)] out TCompiled? License, out ImmutableArray<Exception> Errors) {
            var ret = false;

            if(TryParse(LicenseText, out License, out Errors))
            {
                if (TryValidate(License, out Errors))
                {
                    AddLicense(License);
                    ret = true;
                }
            }

            return ret;
        }

        public TCompiled? TryLoad(string LicenseText)
        {
            TryLoad(LicenseText, out var ret);

            return ret;
        }



        public void Unload(TCompiled? License) {
            RemoveLicense(License);
        }


        public virtual IEnumerable<Exception> Validate(TCompiled License) {
            yield break;
        }


        public bool TryValidate(string LicenseText) {
            return TryValidate(LicenseText, out _);
        }

        public bool TryValidate(string LicenseText, [NotNullWhen(true)] out TCompiled? License) {
            return TryValidate(LicenseText, out License, out _);
        }

        public bool TryValidate(string LicenseText, [NotNullWhen(true)] out TCompiled? License, out ImmutableArray<Exception> Errors) {
            var ret = false;
            License = default;

            if (TryParse(LicenseText, out var ActualLicense, out Errors)) {
                if(TryValidate(ActualLicense, out Errors)) {
                    License = ActualLicense;
                    ret = true;
                }
            }

            return ret;
        }



        public bool TryValidate(TCompiled? License)
        {
            return TryValidate(License, out _);
        }

        public bool TryValidate(TCompiled? License, out ImmutableArray<Exception> Errors)
        {
            var ret = false;
            Errors = ImmutableArray<Exception>.Empty;

            try
            {
                if (License is { })
                {
                    Errors = Validate(License).ToImmutableArray();

                    if (Errors.IsEmpty) {
                        ret = true;
                    }
                    
                }
            } catch(Exception ex)
            {
                ex.Ignore();
            }

            return ret;
        }

        public bool TryParse(string LicenseText){
            return TryParse(LicenseText, out _);
        }

        public bool TryParse(string LicenseText, [NotNullWhen(true)] out TCompiled? License) {
            return TryParse(LicenseText, out License, out _);
        }

        public bool TryParse(string LicenseText, [NotNullWhen(true)] out TCompiled? License, out ImmutableArray<Exception> Errors)
        {
            var ret = false;
            License = default;
            Errors = ImmutableArray<Exception>.Empty;

            foreach (var Parser in Parsers)
            {
                try
                {
                    License = Parser.Parse(LicenseText);
                    ret = true;
                    break;
                } catch (Exception ex)
                {
                    ex.Ignore();
                }
            }
            if(ret == false)
            {
                if (LicenseText.IsBlank())
                {
                    Errors = new Exception[]
                    {
                        new NoLicenseException(),
                    }.ToImmutableArray();
                } else
                {
                    Errors = new Exception[]
                    {
                    new InvalidLicenseException(),
                    }.ToImmutableArray();
                }
            }

            return ret;
        }

        public TCompiled Parse(string LicenseText)
        {
            if(TryParse(LicenseText, out var ret, out var Errors))
            {
                return ret;
            } else
            {
                throw new AggregateException(Errors);
            }

        }


    }

}
