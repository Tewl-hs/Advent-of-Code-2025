def main():
    pos = 50
    part1 = 0
    part2 = 0

    with open("input.txt", "r") as f:
        for line in f:
            line = line.strip()
            if not line:
                continue

            dir = line[0]
            amount = int(line[1:])

            if dir == "L":
                part2 += amount // 100

                if pos != 0 and amount % 100 >= pos:
                    part2 += 1

                pos = (pos - amount) % 100
            else:
                pos += amount

                part2 += pos // 100

                pos = pos % 100

            if pos == 0:
                part1 += 1

    print(f"Part 1: {part1}")
    print(f"Part 2: {part2}")


if __name__ == "__main__":
    main()