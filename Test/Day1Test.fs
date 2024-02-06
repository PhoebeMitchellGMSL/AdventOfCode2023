module Day1Test

open Xunit
open Program
open FluentAssertions

[<Fact>]
let ``Part1Test`` () =
    let input = "1abc2\npqr3stu8vwx\na1b2c3d4e5f\ntreb7uchet"
    let output = Day1 input
    output.Should().Be(142, "because that is the expected output")
    
[<Fact>]
let ``Part2Test``() =
    let input = "two1nine\neightwothree\nabcone2threexyz\nxtwone3four\n4nineeightseven2\nzoneight234\n7pqrstsixteen"
    let output = Day1 input
    output.Should().Be(281, "because that is the expected output")