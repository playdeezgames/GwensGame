Module Status
    Public ShowStatus As Action =
    Sub()
        ClearInputItems()
        AddInputItem("Move...", ShowMoveMenu)
        AddInputItem("Turn...", ShowTurnMenu)
        GetScene(GetX(), GetY()).ShortStatus.Invoke()
        AddInputItem("Main Menu", ShowMainMenu)
    End Sub
End Module
