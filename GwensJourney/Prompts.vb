Module Prompts
    Private ReadOnly prompts As New List(Of String)
    Sub Clear()
        prompts.Clear()
    End Sub
    Sub AddPrompt(prompt As String)
        prompts.Add(prompt)
    End Sub
    Sub ShowPrompts()
        AnsiConsole.WriteLine()
        For Each prompt In prompts
            AnsiConsole.MarkupLine(prompt)
        Next
    End Sub
End Module
