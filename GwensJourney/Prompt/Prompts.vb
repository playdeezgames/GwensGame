Module Prompts
    Private ReadOnly prompts As New List(Of String)
    Sub Clear()
        prompts.Clear()
    End Sub
    Sub Add(prompt As String)
        prompts.Add(prompt)
    End Sub
    Sub Show()
        AnsiConsole.WriteLine()
        For Each prompt In prompts
            AnsiConsole.MarkupLine(prompt)
        Next
    End Sub
End Module
