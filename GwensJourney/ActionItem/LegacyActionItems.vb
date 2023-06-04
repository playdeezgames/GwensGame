Friend Module LegacyActionItems
    Private ReadOnly _actionItems As New List(Of IActionItem)
    Friend Sub Clear()
        _actionItems.Clear()
    End Sub
    Friend Sub Add(text As String, action As Action)
        _actionItems.Add(New ActionItem(text, action))
    End Sub
    Friend Function Choose() As Boolean
        If _actionItems.Count() = 1 Then
            _actionItems(0).Perform()
            Return True
        End If
        Dim index As Integer = 1
        For Each actionItem In _actionItems
            AnsiConsole.MarkupLine($"[silver]{index})[/] {actionItem.DisplayText}")
            index += 1
        Next
        AnsiConsole.WriteLine()
        Dim itemIndex As Integer
        Do
            itemIndex = AnsiConsole.Ask(Of Integer)("[grey]>[/]")
            If itemIndex < 1 OrElse itemIndex > _actionItems.Count Then
                AnsiConsole.WriteLine()
                AnsiConsole.MarkupLine($"[red]Please enter a number between 1 and {_actionItems.Count}.[/]")
            End If
        Loop Until itemIndex >= 1 AndAlso itemIndex <= _actionItems.Count
        _actionItems(itemIndex - 1).Perform()
        Return True
    End Function
    Friend Function HasAny() As Boolean
        Return _actionItems.Any()
    End Function
End Module
