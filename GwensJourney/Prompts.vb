Module Prompts
    Private ReadOnly prompts As New List(Of String)
    Sub ClearPrompts()
        prompts.Clear()
    End Sub
    Sub AddPrompt(prompt As String)
        prompts.Add(prompt)
    End Sub
    Sub ShowPrompts()
        Console.WriteLine()
        For Each prompt In prompts
            Console.WriteLine(prompt)
        Next
    End Sub
End Module
