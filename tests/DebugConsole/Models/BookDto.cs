﻿namespace Stio.DebugConsole.Models;

public class BookDto
{
    public BookId Id { get; set; } = new();

    public string? Name { get; set; }
}