Public Class Prompts
    Implements IPrompts
    Private ReadOnly prompts As New List(Of String)
    Public Function Clear() As IPrompts Implements IPrompts.Clear
        prompts.Clear()
        Return Me
    End Function
    Public Function Add(prompt As String) As IPrompts Implements IPrompts.Add
        prompts.Add(prompt)
        Return Me
    End Function
    Public Function Show() As IPrompts Implements IPrompts.Show
        AnsiConsole.WriteLine()
        For Each prompt In prompts
            AnsiConsole.MarkupLine(prompt)
        Next
        Return Me
    End Function
End Class
