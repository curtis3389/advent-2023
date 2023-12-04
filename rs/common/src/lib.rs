use anyhow::{anyhow, Result};
use regex::Regex;
use std::fs::File;
use std::io;
use std::io::BufRead;
use std::path::Path;
use std::str::FromStr;

/// This is from here: https://doc.rust-lang.org/stable/rust-by-example/std_misc/file/read_lines.html
pub fn read_lines<P: AsRef<Path>>(filename: P) -> io::Result<io::Lines<io::BufReader<File>>> {
    let file = File::open(filename)?;
    Ok(io::BufReader::new(file).lines())
}

/// Parses the calibration value from the given line.
///
/// The calibration value is a 2-digit number from the first and last digits in a line.
///
/// # Examples
///
/// ```
/// # use common::parse_calibration_value;
/// assert_eq!(12, parse_calibration_value("1abc2").unwrap());
/// assert_eq!(38, parse_calibration_value("pqr3stu8vwx").unwrap());
/// assert_eq!(15, parse_calibration_value("a1b2c3d4e5f").unwrap());
/// assert_eq!(77, parse_calibration_value("treb7uchet").unwrap());
/// ```
pub fn parse_calibration_value(line: &str) -> Result<u32> {
    let is_digit = |c: &char| c.is_numeric();
    let first = line
        .chars()
        .filter(is_digit)
        .next()
        .ok_or(anyhow!("No first digit found!"))?;
    let last = line
        .chars()
        .rev()
        .filter(is_digit)
        .next()
        .ok_or(anyhow!("No list digit found!"))?;
    Ok(u32::from_str(format!("{}{}", first, last).as_str())?)
}

pub fn better_parse_calibration_value(line: &str) -> Result<u32> {
    let first_regex = Regex::new(r"(\d|one|two|three|four|five|six|seven|eight|nine|zero)")?;
    let last_regex = Regex::new(r"(\d|eno|owt|eerht|ruof|evif|xis|neves|thgie|enin|orez)")?;
    let first = to_digit(
        &first_regex
            .captures(line)
            .ok_or(anyhow!("No digits found with regex!"))?[1],
    )?;
    let last = to_digit(
        &last_regex
            .captures(line.chars().rev().collect::<String>().as_str())
            .ok_or(anyhow!("No digits found with regex!"))?[1],
    )?;
    Ok(u32::from_str(format!("{}{}", first, last).as_str())?)
}

fn to_digit(s: &str) -> Result<char> {
    match s {
        "1" | "one" | "eno" => Ok('1'),
        "2" | "two" | "owt" => Ok('2'),
        "3" | "three" | "eerht" => Ok('3'),
        "4" | "four" | "ruof" => Ok('4'),
        "5" | "five" | "evif" => Ok('5'),
        "6" | "six" | "xis" => Ok('6'),
        "7" | "seven" | "neves" => Ok('7'),
        "8" | "eight" | "thgie" => Ok('8'),
        "9" | "nine" | "enin" => Ok('9'),
        "0" | "zero" | "orez" => Ok('0'),
        _ => Err(anyhow!("Unknown digit string: {}", s)),
    }
}

pub fn add(left: usize, right: usize) -> usize {
    left + right
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn it_works() {
        let result = add(2, 2);
        assert_eq!(result, 4);
    }
}
