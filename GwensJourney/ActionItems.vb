Module ActionItems
    Private ReadOnly _actionItems As New List(Of IActionItem)
    Sub ClearActions()
        _actionItems.Clear()
    End Sub
    Sub AddAction(text As String, action As Action)
        _actionItems.Add(New ActionItem(text, action))
    End Sub
    Function SelectAction() As Boolean
        If _actionItems.Count() = 1 Then
            _actionItems(0).Perform()
            Return True
        End If
        Dim index As Integer = 1
        For Each actionItem In _actionItems
            AnsiConsole.MarkupLine($"{index}) {actionItem.DisplayText}")
            index += 1
        Next
        AnsiConsole.WriteLine()
        AnsiConsole.Markup(">")
        Dim input As String = Console.ReadLine()
        Dim itemIndex As Integer
        If Integer.TryParse(input, itemIndex) Then
            itemIndex = itemIndex - 1
            If itemIndex < _actionItems.Count Then
                _actionItems(itemIndex).Perform()
                Return True
            End If
        End If
        AnsiConsole.MarkupLine($"[red]Please enter a number between 1 and {_actionItems.Count}.[/]")
        Return False
    End Function
    Function HasActions() As Boolean
        Return _actionItems.Any()
    End Function
End Module
