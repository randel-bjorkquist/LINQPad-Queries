<Query Kind="Program">
  <Namespace>System.ComponentModel.DataAnnotations</Namespace>
</Query>

/// <summary>
/// Simulates text editing and uno operations using Memento Pattern
/// </summary>
void Main()
{
  TextEditor editor         = new TextEditor();
  EditorCaretaker caretaker = new EditorCaretaker();
  
  editor.SetContent("First Line of Text");
  editor.Dump();
  caretaker.SaveState(editor);
  caretaker.Dump();
  
  editor.SetContent("Second Line of Text");
  editor.Dump();
  caretaker.SaveState(editor);
  caretaker.Dump();
  
  //undo the last change ...
  caretaker.Undo(editor);
  caretaker.Dump();
}

/// <summary>
/// Holds the state of an object. It doesn't perform any operation.
/// Its only job is to store data.
/// </summary>
public class Memento
{
  public string Content { get; private set; }
  
  public Memento(string content)
  {
    Content = content;
  }
}

/// <summary>
/// The main object whose state needs to be saved and restored. It creates
/// a Memento containing a snapshot of its current internal current internal
/// state and can also use the Memento to restore its internal state.
/// </summary>
public class TextEditor
{
  private string _content;
  
  public void SetContent(string content)
    => _content = content;
    
  public Memento Save()
    => new Memento(_content);
    
  public void Restore(Memento memento)
    => _content = memento.Content;
}

/// <summary>
/// Responsible for the memento's safekeeping. It keeps track of the 
/// memento(s) but never modifies or examines the contents of memento.
/// </summary
public class EditorCaretaker
{
  private Stack<Memento> _history = new();
  
  public void SaveState(TextEditor editor)
    => _history.Push(editor.Save());
    
  public void Undo(TextEditor editor)
  {
    if(_history.Count > 0) 
      editor.Restore(_history.Pop());
  }
}
