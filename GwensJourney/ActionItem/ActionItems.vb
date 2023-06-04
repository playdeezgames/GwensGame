Public Class ActionItems
    Implements IActionItems
    Private ReadOnly actionItems As New List(Of IActionItem)
    Public Function Clear() As IActionItems Implements IActionItems.Clear
        actionItems.Clear()
        Return Me
    End Function
    Public Function Add(text As String, action As Action) As IActionItems Implements IActionItems.Add
        actionItems.Add(New ActionItem(text, action))
        Return Me
    End Function
    Public Function Choose() As Boolean Implements IActionItems.Choose
        If actionItems.Count() = 1 Then
            actionItems(0).Perform()
            Return True
        End If
        Dim index As Integer = 1
        For Each actionItem In actionItems
            AnsiConsole.MarkupLine($"[silver]{index})[/] {actionItem.DisplayText}")
            index += 1
        Next
        AnsiConsole.WriteLine()
        Dim itemIndex As Integer
        Do
            itemIndex = AnsiConsole.Ask(Of Integer)("[grey]>[/]")
            If itemIndex < 1 OrElse itemIndex > actionItems.Count Then
                AnsiConsole.WriteLine()
                AnsiConsole.MarkupLine($"[red]Please enter a number between 1 and {actionItems.Count}.[/]")
            End If
        Loop Until itemIndex >= 1 AndAlso itemIndex <= actionItems.Count
        actionItems(itemIndex - 1).Perform()
        Return True
    End Function
    Public Function HasAny() As Boolean Implements IActionItems.HasAny
        Return actionItems.Any()
    End Function
End Class
