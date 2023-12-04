use common::{better_parse_calibration_value, read_lines};

fn main() {
    let sum: u32 = read_lines("/home/curtis/Projects/crh/advent-2023/inputs/day/1/input")
        .unwrap()
        .map(|line| better_parse_calibration_value(line.unwrap().as_str()).unwrap())
        .sum();
    println!("{}", sum);
}
