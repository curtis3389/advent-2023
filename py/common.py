import re


def calibration_value(line):
    digits = list(filter(lambda c: c.isdigit(), line))
    left = digits[0]
    right = digits[-1]
    return int(f'{left}{right}')


def to_digit(s):
    match s:
        case '1' | 'one' | 'eno':
            return '1'
        case '2' | 'two' | 'owt':
            return '2'
        case '3' | 'three' | 'eerht':
            return '3'
        case '4' | 'four' | 'ruof':
            return '4'
        case '5' | 'five' | 'evif':
            return '5'
        case '6' | 'six' | 'xis':
            return '6'
        case '7' | 'seven' | 'neves':
            return '7'
        case '8' | 'eight' | 'thgie':
            return '8'
        case '9' | 'nine' | 'enin':
            return '9'
        case '0' | 'zero' | 'orez':
            return '0'


def better_calibration_value(line):
    digit_regex = re.compile(r'(\d|one|two|three|four|five|six|seven|eight|nine|zero)')
    rev_digit_regex = re.compile(r'(\d|eno|owt|eerht|ruof|evif|xis|neves|thgie|enin|orez)')
    left = to_digit(digit_regex.search(line).groups()[0])
    right = to_digit(rev_digit_regex.search(''.join(reversed(line))).groups()[0])
    return int(f'{left}{right}')
