<Query Kind="Statements" />

//-------------------------------------------------------------------------------------------------
#load ".\FeatureFlagValueSource"

//-------------------------------------------------------------------------------------------------
public readonly record struct FeatureFlagResolution( string FeatureFlag
                                                    ,string? TenantCode
                                                    ,FeatureFlagValueSource Source
                                                    ,int Priority
                                                    ,bool Value );