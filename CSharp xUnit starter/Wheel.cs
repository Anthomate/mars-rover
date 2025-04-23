using System;

namespace CSharp_xUnit_starter;

public class Wheel
{
    public Guid Id { get; set; }
    public string Type { get; set; }
    public Rover Rover { get; set; }
}