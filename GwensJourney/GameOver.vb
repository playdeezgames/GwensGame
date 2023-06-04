Friend Module GameOver
    Friend Sub Run(engine As IEngine)
        engine.Prompts.Clear()
        LegacyPrompts.Add(engine, $"[red]Yer dead![/]")
        LegacyActionItems.Clear()
        LegacyActionItems.Add("That sucks!", Sub() MainMenu.Run(engine))
    End Sub
End Module
