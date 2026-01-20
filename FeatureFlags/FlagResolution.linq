<Query Kind="Program" />

//-------------------------------------------------------------------------------------------------
#load "..\FeatureFlags\FlagValueSource"

//-------------------------------------------------------------------------------------------------
void Main()
{  
}

//-------------------------------------------------------------------------------------------------
public readonly record struct FlagResolution( string FeatureFlag
                                             ,string? TenantCode
                                             ,FlagValueSource Source
                                             ,int Priority
                                             ,bool Value );
