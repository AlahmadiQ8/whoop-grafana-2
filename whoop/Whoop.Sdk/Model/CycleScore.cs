/*
 * WHOOP API
 *
 * No description provided (generated by Openapi Generator https://github.com/openapitools/openapi-generator)
 *
 * The version of the OpenAPI document: 1.0.1
 * Generated by: https://github.com/openapitools/openapi-generator.git
 */


using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;
using OpenAPIDateConverter = Whoop.Sdk.Client.OpenAPIDateConverter;

namespace Whoop.Sdk.Model
{
    /// <summary>
    /// WHOOP&#39;s measurements and evaluation of the cycle. Only present if the score state is &#x60;SCORED&#x60;
    /// </summary>
    [DataContract(Name = "CycleScore")]
    public partial class CycleScore : IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CycleScore" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected CycleScore() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="CycleScore" /> class.
        /// </summary>
        /// <param name="strain">WHOOP metric of the cardiovascular load - the level of strain  on the user&#39;s cardiovascular system based on the user&#39;s heart rate during the cycle. Strain is scored on a scale from 0 to 21. (required).</param>
        /// <param name="kilojoule">Kilojoules the user expended during the cycle. (required).</param>
        /// <param name="averageHeartRate">The user&#39;s average heart rate during the cycle. (required).</param>
        /// <param name="maxHeartRate">The user&#39;s max heart rate during the cycle. (required).</param>
        public CycleScore(float strain = default(float), float kilojoule = default(float), int averageHeartRate = default(int), int maxHeartRate = default(int))
        {
            this.Strain = strain;
            this.Kilojoule = kilojoule;
            this.AverageHeartRate = averageHeartRate;
            this.MaxHeartRate = maxHeartRate;
        }

        /// <summary>
        /// WHOOP metric of the cardiovascular load - the level of strain  on the user&#39;s cardiovascular system based on the user&#39;s heart rate during the cycle. Strain is scored on a scale from 0 to 21.
        /// </summary>
        /// <value>WHOOP metric of the cardiovascular load - the level of strain  on the user&#39;s cardiovascular system based on the user&#39;s heart rate during the cycle. Strain is scored on a scale from 0 to 21.</value>
        /// <example>5.2951527</example>
        [DataMember(Name = "strain", IsRequired = true, EmitDefaultValue = true)]
        public float Strain { get; set; }

        /// <summary>
        /// Kilojoules the user expended during the cycle.
        /// </summary>
        /// <value>Kilojoules the user expended during the cycle.</value>
        /// <example>8288.297</example>
        [DataMember(Name = "kilojoule", IsRequired = true, EmitDefaultValue = true)]
        public float Kilojoule { get; set; }

        /// <summary>
        /// The user&#39;s average heart rate during the cycle.
        /// </summary>
        /// <value>The user&#39;s average heart rate during the cycle.</value>
        /// <example>68</example>
        [DataMember(Name = "average_heart_rate", IsRequired = true, EmitDefaultValue = true)]
        public int AverageHeartRate { get; set; }

        /// <summary>
        /// The user&#39;s max heart rate during the cycle.
        /// </summary>
        /// <value>The user&#39;s max heart rate during the cycle.</value>
        /// <example>141</example>
        [DataMember(Name = "max_heart_rate", IsRequired = true, EmitDefaultValue = true)]
        public int MaxHeartRate { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class CycleScore {\n");
            sb.Append("  Strain: ").Append(Strain).Append("\n");
            sb.Append("  Kilojoule: ").Append(Kilojoule).Append("\n");
            sb.Append("  AverageHeartRate: ").Append(AverageHeartRate).Append("\n");
            sb.Append("  MaxHeartRate: ").Append(MaxHeartRate).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public virtual string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented);
        }

        /// <summary>
        /// To validate all properties of the instance
        /// </summary>
        /// <param name="validationContext">Validation context</param>
        /// <returns>Validation Result</returns>
        IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }

}