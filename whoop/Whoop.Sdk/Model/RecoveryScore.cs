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
    /// WHOOP&#39;s measurements and evaluation of the recovery. Only present if the Recovery State is &#x60;SCORED&#x60;
    /// </summary>
    [DataContract(Name = "RecoveryScore")]
    public partial class RecoveryScore : IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RecoveryScore" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected RecoveryScore() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="RecoveryScore" /> class.
        /// </summary>
        /// <param name="userCalibrating">True if the user is still calibrating and not enough data is available in WHOOP to provide an accurate recovery. (required).</param>
        /// <param name="varRecoveryScore">Percentage (0-100%) that reflects how well prepared the user&#39;s body is to take on Strain. The Recovery score is a measure of the user body&#39;s \&quot;return to baseline\&quot; after a stressor. (required).</param>
        /// <param name="restingHeartRate">The user&#39;s resting heart rate. (required).</param>
        /// <param name="hrvRmssdMilli">The user&#39;s Heart Rate Variability measured using Root Mean Square of Successive Differences (RMSSD), in milliseconds. (required).</param>
        /// <param name="spo2Percentage">The percentage of oxygen in the user&#39;s blood. Only present if the user is on 4.0 or greater..</param>
        /// <param name="skinTempCelsius">The user&#39;s skin temperature, in Celsius. Only present if the user is on 4.0 or greater..</param>
        public RecoveryScore(bool userCalibrating = default(bool), float varRecoveryScore = default(float), float restingHeartRate = default(float), float hrvRmssdMilli = default(float), float spo2Percentage = default(float), float? skinTempCelsius = default(float?))
        {
            this.UserCalibrating = userCalibrating;
            this.VarRecoveryScore = varRecoveryScore;
            this.RestingHeartRate = restingHeartRate;
            this.HrvRmssdMilli = hrvRmssdMilli;
            this.Spo2Percentage = spo2Percentage;
            this.SkinTempCelsius = skinTempCelsius;
        }

        /// <summary>
        /// True if the user is still calibrating and not enough data is available in WHOOP to provide an accurate recovery.
        /// </summary>
        /// <value>True if the user is still calibrating and not enough data is available in WHOOP to provide an accurate recovery.</value>
        /// <example>false</example>
        [DataMember(Name = "user_calibrating", IsRequired = true, EmitDefaultValue = true)]
        public bool UserCalibrating { get; set; }

        /// <summary>
        /// Percentage (0-100%) that reflects how well prepared the user&#39;s body is to take on Strain. The Recovery score is a measure of the user body&#39;s \&quot;return to baseline\&quot; after a stressor.
        /// </summary>
        /// <value>Percentage (0-100%) that reflects how well prepared the user&#39;s body is to take on Strain. The Recovery score is a measure of the user body&#39;s \&quot;return to baseline\&quot; after a stressor.</value>
        /// <example>44</example>
        [DataMember(Name = "recovery_score", IsRequired = true, EmitDefaultValue = true)]
        public float VarRecoveryScore { get; set; }

        /// <summary>
        /// The user&#39;s resting heart rate.
        /// </summary>
        /// <value>The user&#39;s resting heart rate.</value>
        /// <example>64</example>
        [DataMember(Name = "resting_heart_rate", IsRequired = true, EmitDefaultValue = true)]
        public float RestingHeartRate { get; set; }

        /// <summary>
        /// The user&#39;s Heart Rate Variability measured using Root Mean Square of Successive Differences (RMSSD), in milliseconds.
        /// </summary>
        /// <value>The user&#39;s Heart Rate Variability measured using Root Mean Square of Successive Differences (RMSSD), in milliseconds.</value>
        /// <example>31.813562</example>
        [DataMember(Name = "hrv_rmssd_milli", IsRequired = true, EmitDefaultValue = true)]
        public float HrvRmssdMilli { get; set; }

        /// <summary>
        /// The percentage of oxygen in the user&#39;s blood. Only present if the user is on 4.0 or greater.
        /// </summary>
        /// <value>The percentage of oxygen in the user&#39;s blood. Only present if the user is on 4.0 or greater.</value>
        /// <example>95.6875</example>
        [DataMember(Name = "spo2_percentage", EmitDefaultValue = false)]
        public float Spo2Percentage { get; set; }

        /// <summary>
        /// The user&#39;s skin temperature, in Celsius. Only present if the user is on 4.0 or greater.
        /// </summary>
        /// <value>The user&#39;s skin temperature, in Celsius. Only present if the user is on 4.0 or greater.</value>
        /// <example>33.7</example>
        [DataMember(Name = "skin_temp_celsius", EmitDefaultValue = true)]
        public float? SkinTempCelsius { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class RecoveryScore {\n");
            sb.Append("  UserCalibrating: ").Append(UserCalibrating).Append("\n");
            sb.Append("  VarRecoveryScore: ").Append(VarRecoveryScore).Append("\n");
            sb.Append("  RestingHeartRate: ").Append(RestingHeartRate).Append("\n");
            sb.Append("  HrvRmssdMilli: ").Append(HrvRmssdMilli).Append("\n");
            sb.Append("  Spo2Percentage: ").Append(Spo2Percentage).Append("\n");
            sb.Append("  SkinTempCelsius: ").Append(SkinTempCelsius).Append("\n");
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
