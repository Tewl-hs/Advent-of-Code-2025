use std::fs::File;
use std::io::{self, BufRead};

fn main() -> io::Result<()> {
    let mut pos: i64 = 50;
    let mut part1: u64 = 0; // Part 1 result
    let mut part2: u64 = 0; // Part 2 result

    let file = File::open("input.txt")?;
    let reader = io::BufReader::new(file);

    for line in reader.lines() {
        let line = line?;
        if line.trim().is_empty() { continue; }

        let dir = line.chars().next().unwrap();
        let amount: i64 = line[1..].parse().expect("Invalid number");

        if dir == 'L' {
            part2 += (amount / 100) as u64;
            if pos != 0 && amount % 100 >= pos {
                part2 += 1;
            }
            
            pos = (pos - amount).rem_euclid(100);
        } else {
            part2 += ((pos + amount) / 100) as u64;

            pos = (pos + amount).rem_euclid(100);
        }

        if pos == 0 {
            part1 += 1;
        }
    }

    println!("Part 1: {}", part1);
    println!("Part 2: {}", part2);

    Ok(())
}
