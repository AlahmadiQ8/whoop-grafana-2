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
    /// Breakdown of time spent in each heart rate zone during the workout.
    /// </summary>
    [DataContract(Name = "ZoneDuration")]
    public partial class ZoneDuration : IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ZoneDuration" /> class.
        /// </summary>
        /// <param name="zoneZeroMilli">Time spent with Heart Rate lower than Zone One [0-50%).</param>
        /// <param name="zoneOneMilli">Time spent in Heart Rate Zone One [50-60%).</param>
        /// <param name="zoneTwoMilli">Time spent in Heart Rate Zone Two [60-70%).</param>
        /// <param name="zoneThreeMilli">Time spent in Heart Rate Zone Three [70-80%).</param>
        /// <param name="zoneFourMilli">Time spent in Heart Rate Zone Four [80-90%).</param>
        /// <param name="zoneFiveMilli">Time spent in Heart Rate Zone Five [90-100%).</param>
        public ZoneDuration(int zoneZeroMilli = default(int), int zoneOneMilli = default(int), int zoneTwoMilli = default(int), int zoneThreeMilli = default(int), int zoneFourMilli = default(int), int zoneFiveMilli = default(int))
        {
            this.ZoneZeroMilli = zoneZeroMilli;
            this.ZoneOneMilli = zoneOneMilli;
            this.ZoneTwoMilli = zoneTwoMilli;
            this.ZoneThreeMilli = zoneThreeMilli;
            this.ZoneFourMilli = zoneFourMilli;
            this.ZoneFiveMilli = zoneFiveMilli;
        }

        /// <summary>
        /// Time spent with Heart Rate lower than Zone One [0-50%)
        /// </summary>
        /// <value>Time spent with Heart Rate lower than Zone One [0-50%)</value>
        /// <example>13458</example>
        [DataMember(Name = "zone_zero_milli", EmitDefaultValue = false)]
        public int ZoneZeroMilli { get; set; }

        /// <summary>
        /// Time spent in Heart Rate Zone One [50-60%)
        /// </summary>
        /// <value>Time spent in Heart Rate Zone One [50-60%)</value>
        /// <example>389370</example>
        [DataMember(Name = "zone_one_milli", EmitDefaultValue = false)]
        public int ZoneOneMilli { get; set; }

        /// <summary>
        /// Time spent in Heart Rate Zone Two [60-70%)
        /// </summary>
        /// <value>Time spent in Heart Rate Zone Two [60-70%)</value>
        /// <example>388367</example>
        [DataMember(Name = "zone_two_milli", EmitDefaultValue = false)]
        public int ZoneTwoMilli { get; set; }

        /// <summary>
        /// Time spent in Heart Rate Zone Three [70-80%)
        /// </summary>
        /// <value>Time spent in Heart Rate Zone Three [70-80%)</value>
        /// <example>71137</example>
        [DataMember(Name = "zone_three_milli", EmitDefaultValue = false)]
        public int ZoneThreeMilli { get; set; }

        /// <summary>
        /// Time spent in Heart Rate Zone Four [80-90%)
        /// </summary>
        /// <value>Time spent in Heart Rate Zone Four [80-90%)</value>
        /// <example>0</example>
        [DataMember(Name = "zone_four_milli", EmitDefaultValue = false)]
        public int ZoneFourMilli { get; set; }

        /// <summary>
        /// Time spent in Heart Rate Zone Five [90-100%)
        /// </summary>
        /// <value>Time spent in Heart Rate Zone Five [90-100%)</value>
        /// <example>0</example>
        [DataMember(Name = "zone_five_milli", EmitDefaultValue = false)]
        public int ZoneFiveMilli { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class ZoneDuration {\n");
            sb.Append("  ZoneZeroMilli: ").Append(ZoneZeroMilli).Append("\n");
            sb.Append("  ZoneOneMilli: ").Append(ZoneOneMilli).Append("\n");
            sb.Append("  ZoneTwoMilli: ").Append(ZoneTwoMilli).Append("\n");
            sb.Append("  ZoneThreeMilli: ").Append(ZoneThreeMilli).Append("\n");
            sb.Append("  ZoneFourMilli: ").Append(ZoneFourMilli).Append("\n");
            sb.Append("  ZoneFiveMilli: ").Append(ZoneFiveMilli).Append("\n");
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