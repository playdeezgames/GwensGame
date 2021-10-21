Module Prompt
    Private prompts As New List(Of String)
    Sub AddPrompt(prompt As String)
        prompts.Add(prompt)
    End Sub
    Sub ShowPrompts()
        For Each prompt In prompts
            Console.WriteLine(prompt)
        Next
    End Sub
    Sub ClearPrompts()
        prompts.Clear()
    End Sub
End Module
