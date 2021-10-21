Module Status
    Public ShowStatus As Action =
    Sub()
        ClearInputItems()
        AddPrompt($"You are at ({Data.GetX()}, {Data.GetY()}) and facing {GetDirectionName(Data.GetFacing())}.")
        AddInputItem("Move...", ShowMoveMenu)
        AddInputItem("Main Menu", ShowMainMenu)
    End Sub
End Module
