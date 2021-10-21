Module Inputter
    Private items As New Dictionary(Of String, InputItem)
    Sub AddInputItem(text As String, action As Action)
        Dim count = items.Count + 1
        items(count.ToString()) = New InputItem(text, action)
    End Sub
    Sub ClearInputItems()
        items.Clear()
    End Sub
    Function HasInputItems()
        Return items.Any()
    End Function
    Sub ReadInput()
        For Each item In items
            Console.WriteLine("{0}) {1}", item.Key, item.Value.Text)
        Next
        While True
            Console.WriteLine()
            Console.Write(">")
            Dim input As String = Console.ReadLine()
            If items.ContainsKey(input) Then
                items(input).Stuff.Invoke()
                Exit Sub
            End If
            Console.WriteLine("Please pick a number between 0 and {0}!", items.Count)
        End While
    End Sub
End Module
