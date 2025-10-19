<Query Kind="Program" />

void Main()
{
  //var vocabulary = VocabularyKeyVisibility.NotSet
  //               | VocabularyKeyVisibility.Visible
  //               | VocabularyKeyVisibility.Hidden
  //               | VocabularyKeyVisibility.ExcludeFromHashing
  //               | VocabularyKeyVisibility.HiddenInFrontendUI
  //               | VocabularyKeyVisibility.IgnoreCaseInHashing;

  //Visible, Hidden, ExcludeFromHashing
  //var vocabulary = VocabularyKeyVisibility.Visible              //Appears in the .Dump()
  //							 | VocabularyKeyVisibility.Hidden               //Appears in the .Dump()
  //               | VocabularyKeyVisibility.ExcludeFromHashing;  //Appears in the .Dump()
  
  //Hidden, ExcludeFromHashing, HiddenInFrontendUI
  var vocabulary = VocabularyKeyVisibility.Visible              //DOES NOT appears in the .Dump()
								 | VocabularyKeyVisibility.Hidden               //Appears in the .Dump()
                 | VocabularyKeyVisibility.ExcludeFromHashing   //Appears in the .Dump()
                 | VocabularyKeyVisibility.HiddenInFrontendUI;  //Appears in the .Dump()
  
	vocabulary.Dump("vocabulary");
  ((int)vocabulary).Dump("(int)vocabulary");
  
  //Console.WriteLine("Integer (int) Values:");
  //((int)VocabularyKeyVisibility.NotSet).Dump();
	//((int)VocabularyKeyVisibility.Visible).Dump();
	//((int)VocabularyKeyVisibility.Hidden).Dump();
	//((int)VocabularyKeyVisibility.ExcludeFromHashing).Dump();
	//((int)VocabularyKeyVisibility.HiddenInFrontendUI).Dump();
	//((int)VocabularyKeyVisibility.IgnoreCaseInHashing).Dump();

  //Console.WriteLine();
  //Console.WriteLine("Hexadecimal (hex) Values:");
  //VocabularyKeyVisibility.NotSet.ToString("X").Dump();
  //VocabularyKeyVisibility.Visible.ToString("X").Dump();
  //VocabularyKeyVisibility.Hidden.ToString("X").Dump();
  //VocabularyKeyVisibility.ExcludeFromHashing.ToString("X").Dump();
  //VocabularyKeyVisibility.HiddenInFrontendUI.ToString("X").Dump();
  //VocabularyKeyVisibility.IgnoreCaseInHashing.ToString("X").Dump();
}

[Flags]
public enum VocabularyKeyVisibility
{
	NotSet              = 0x0
 ,Visible             = 0x1
 ,Hidden              = 0x2
 ,ExcludeFromHashing  = 0x4
 ,HiddenInFrontendUI  = 0x9
 ,IgnoreCaseInHashing = 0x10
}

//ORIGINAL Decompiled Definition
//[Flags]
//public enum VocabularyKeyVisibility
//{
//   NotSet               = 0x0
//  ,Hidden               = 0x2
//  ,HiddenInFrontendUI   = 0x9
//  ,Visible              = 0x1
//  ,ExcludeFromHashing   = 0x4
//  ,IgnoreCaseInHashing  = 0x10
//}
