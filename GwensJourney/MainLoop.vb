Friend Module MainLoop
    Friend Sub Run()
        While HasActions()
            Do
                ShowPrompts()
            Loop Until SelectAction()
        End While
    End Sub
End Module
