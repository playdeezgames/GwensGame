﻿Public Class Prompts
    Implements IPrompts
    Private ReadOnly prompts As New List(Of String)
    Public Sub Clear() Implements IPrompts.Clear
        prompts.Clear()
    End Sub
    Public Sub Add(prompt As String) Implements IPrompts.Add
        prompts.Add(prompt)
    End Sub
    Public Sub Show() Implements IPrompts.Show
        AnsiConsole.WriteLine()
        For Each prompt In prompts
            AnsiConsole.MarkupLine(prompt)
        Next
    End Sub
End Class
