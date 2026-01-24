<Query Kind="Statements" />

//-------------------------------------------------------------------------------------------------
#load ".\FlagValueSource"

//-------------------------------------------------------------------------------------------------
public readonly record struct FlagResolution( string FeatureFlag
                                             ,string? TenantCode
                                             ,FlagValueSource Source
                                             ,int Priority
                                             ,bool Value );