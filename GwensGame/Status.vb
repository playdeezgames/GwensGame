Module Status
    Public ShowStatus As Action =
    Sub()
        ClearPrompts()
        ClearInputItems()
        AddPrompt("")
        AddPrompt($"You are at ({Data.GetX()}, {Data.GetY()}) and facing {GetDirectionName(Data.GetFacing())}.")
        AddInputItem("Main Menu", ShowMainMenu)
    End Sub
End Module
