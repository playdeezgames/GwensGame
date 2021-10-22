Module Program
    Private actionItems As New List(Of IActionItem)
    Sub ClearActions()
        actionItems.Clear()
    End Sub
    Sub AddAction(text As String, action As Action)
        actionItems.Add(New ActionItem(text, action))
    End Sub
    Sub SelectAction()
        If actionItems.Any() Then
            Dim index As Integer = 1
            For Each actionItem In actionItems
                Console.WriteLine($"{index}) {actionItem.DisplayText}")
                index += 1
            Next
            Console.WriteLine()
            Console.Write(">")
            Dim input As String = Console.ReadLine()
            Dim itemIndex As Integer
            If Integer.TryParse(input, itemIndex) Then
                itemIndex = itemIndex - 1
                If itemIndex < actionItems.Count Then
                    actionItems(itemIndex).Perform()
                    Exit Sub
                End If
            End If
            Console.WriteLine($"Please enter a number between 1 and {actionItems.Count}.")
        End If
    End Sub
    Sub Welcome()
        Console.WriteLine("Welcome to Gwen's Journey")
    End Sub
    Sub GoodBye()
        Console.WriteLine()
        Console.WriteLine("Thank you for playing Gwen's Journey")
    End Sub
    Sub MainMenu()
        Dim done As Boolean = False
        While Not done
            Console.WriteLine()
            Console.WriteLine("Main Menu:")
            ClearActions()
            AddAction("Quit", Sub() done = True)
            SelectAction()
        End While
    End Sub
    Sub Main(args As String())
        Welcome()
        MainMenu()
        GoodBye()
    End Sub
End Module
