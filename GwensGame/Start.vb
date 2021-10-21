Module Start
    Public StartGame As Action =
        Sub()
            Console.WriteLine()
            Console.Write("What is yer name?")
            ResetData(Console.ReadLine())
            ClearPrompts()
            ShowStatus()
        End Sub
End Module
