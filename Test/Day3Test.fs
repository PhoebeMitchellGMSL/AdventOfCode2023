module Day3Test

open Xunit
open FluentAssertions
open Day3Part1

let input = ["467..114..";
            "...*......";
            "..35..633.";
            "......#...";
            "617*......";
            ".....+.58.";
            "..592.....";
            "......755.";
            "...$.*....";
            ".664.598.."]

[<Fact>]
let ``Part1Test`` () =
    let output = Day3Part1 input
    output.Should().Be(4361, "")
    
[<Fact>]
let ``Part2Test``() =
    0