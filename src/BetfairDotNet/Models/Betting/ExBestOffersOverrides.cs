using BetfairDotNet.Enums.Betting;
using System.Text.Json.Serialization;


namespace BetfairDotNet.Models.Betting;

/// <summary>
/// Options to alter the default representation of best offer prices.
/// </summary>
public sealed class ExBestOffersOverrides {

    /// <summary>
    /// Represents the maximum number of prices to return on each side for each runner. 
    /// </summary>
    /// <remarks>
    /// Defaults to 3 if unspecified. The maximum depth returned is 10.
    /// </remarks>
    [JsonPropertyName("bestPricesDepth")]
    public int BestPricesDepth { get; set; }

    /// <summary>
    /// Represents the model used for rolling up available sizes.
    /// </summary>
    /// <remarks>
    /// Defaults to the STAKE rollup model with a rollupLimit equivalent to the minimum stake in the specified currency if unspecified.
    /// </remarks>
    [JsonPropertyName("rollupModel")]
    public RollupModelEnum RollUpModel { get; set; }

    /// <summary>
    /// Indicates the volume limit when rolling up returned sizes.
    /// </summary>
    /// <remarks>
    /// The definition of the limit varies based on the <see cref="RollupModelEnum"/>. Defaults to the minimum stake if not provided. 
    /// This value is ignored if no rollup model is specified.
    /// </remarks>
    [JsonPropertyName("rollupLimit")]
    public int RollUpLimit { get; set; }

    /// <summary>
    /// The threshold at which the rollup model switches from stake-based to liability-based.
    /// </summary>
    /// <remarks>
    /// Only applicable when <see cref="RollupModelEnum"/> is MANAGED_LIABILITY. The rollup switches at the smallest lay price 
    /// greater than or equal to this threshold. The exact service level default is TBD. Not yet supported.
    /// </remarks>
    [JsonPropertyName("rollupLiabilityThreshold")]
    public double RollUpLiabilityThreshold { get; set; }

    /// <summary>
    /// Represents the factor of liability deemed acceptable by the user.
    /// </summary>
    /// <remarks>
    /// Only applicable when <see cref="RollupModelEnum"/> is MANAGED_LIABILITY. After reaching the <see cref="RollUpLiabilityThreshold"/> price,
    /// subsequent volumes will be rolled up to a minimum value where the liability meets or exceeds this factor times the <see cref="RollUpLimit"/>.
    /// The service level default is 5. Not yet supported.
    /// </remarks>
    [JsonPropertyName("rollupLiabilityFactor")]
    public int RollUpLiabilityFactor { get; set; }
}
