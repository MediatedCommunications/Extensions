using PostSharp.Aspects;
using PostSharp.Extensibility;
using PostSharp.Serialization;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using System.Text.Json.Serialization;

namespace System.Text.Json {
    [MulticastAttributeUsage(Inheritance = MulticastInheritance.Multicast)]
    [PSerializable]
    public class OptionalJsonIgnoreAttribute : TypeLevelAspect, IAspectProvider {
        // This method is called at build time and should just provide other aspects. 


        static OptionalJsonIgnoreAttribute() {
            {
                var Member = typeof(OptionalJsonIgnoreAttribute).GetProperty(nameof(AttributesToIntroduce), BindingFlags.NonPublic | BindingFlags.Static);
                var Data = Member?.GetCustomAttributesData() ?? Array.Empty<CustomAttributeData>();
                AttributesToIntroduce = Data.ToImmutableList();
            }

            {
                AspectsToIntroduce = (
                    from x in AttributesToIntroduce
                    select new CustomAttributeIntroductionAspect(x)
                    ).ToImmutableList();
            }

        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        private static ImmutableList<CustomAttributeData> AttributesToIntroduce { get; }
        private static ImmutableList<CustomAttributeIntroductionAspect> AspectsToIntroduce { get; }


        public IEnumerable<AspectInstance> ProvideAspects(object targetElement) {
            Type targetType = (Type)targetElement;

            // Add a DataMember attribute to every relevant property. 
            var Properties = (
                from x in targetType.GetProperties(BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Instance)
                where x.PropertyType.IsGenericType && x.PropertyType.GetGenericTypeDefinition() == typeof(Optional<>)
                select x
                ).ToList();

            foreach (var Property in Properties) {
                foreach (var AspectToIntroduce in AspectsToIntroduce) {
                    yield return new AspectInstance(Property, AspectToIntroduce);
                }

                
            }
        }
    }


}
