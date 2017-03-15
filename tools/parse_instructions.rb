#!/usr/bin/env ruby
#
# Parse the 6510 instruction data from HTML pages from
# http://unusedino.de/ec64/technical/aay/c64/bmain.htm. The output format is
# a CSV file.

def parse_addressing_mode(value)
  value.scan(/>.*</).last.gsub('<','').gsub('>','').gsub(',','').gsub('(','')
    .gsub(')','').gsub('/Stack','').gsub('ZeroPage','Zeropage')
end

def parse_code(value)
  value.strip[1..3]
end

def parse_int(value)
  value.strip[0]
end

def parse_instruction(instruction, filepath, debug)
  File.foreach(filepath) do |line|
    if line.start_with? '  |  <a href'
      parts = line.split('|')
      if debug
        parts.each_with_index do |part,i|
          print "#{i}: #{part}\n"
        end
      end
      addressing_mode = parse_addressing_mode(parts[1])
      code = parse_code(parts[3])
      size = parse_int(parts[4])
      cycles = parse_int(parts[5])
      puts "#{instruction},#{addressing_mode},#{code},#{cycles},#{size}"
    end
    break if line.include? '65816'
  end
end

def file_path_for(instruction)
  "../data/b#{instruction.downcase}.htm"
end

instructions = [
  'Adc',
  'And',
  'Asl',
  'Bcc',
  'Bcs',
  'Beq',
  'Bit',
  'Bmi',
  'Bne',
  'Bpl',
  'Brk',
  'Bvc',
  'Bvs',
  'Clc',
  'Cld',
  'Cli',
  'Clv',
  'Cmp',
  'Cpx',
  'Cpy',
  'Dec',
  'Dex',
  'Dey',
  'Eor',
  'Inc',
  'Inx',
  'Iny',
  'Jmp',
  'Jsr',
  'Lda',
  'Ldx',
  'Ldy',
  'Lsr',
  'Nop',
  'Ora',
  'Pha',
  'Php',
  'Pla',
  'Plp',
  'Rol',
  'Ror',
  'Rti',
  'Rts',
  'Sbc',
  'Sec',
  'Sed',
  'Sei',
  'Sta',
  'Stx',
  'Sty',
  'Tax',
  'Tay',
  'Tsx',
  'Txa',
  'Txs',
  'Tya',
]

unless ARGV[0].nil? or ARGV[0].empty?
  parse_instruction("debug", ARGV[0], true)
else
  instructions.each do |instruction|
    parse_instruction(instruction, file_path_for(instruction), false)
  end
end
