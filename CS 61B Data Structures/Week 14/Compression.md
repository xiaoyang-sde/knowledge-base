
# Compression

Compression Model #1: Algorithms Operating on Bits.
* Bitstream -> Algorithms Operating on Bits -> Compressed bits C
* Compressed bits C -> Decompression Algorithm -> Bitstream

A lossless compression algorithm require that no information is lost.

## Prefix Free Codes

English text is usually represented by sequences of characters, each 8 bits long. Use fewer than 8 bits for each letter: Have to decide which bit sequences should go with which letters.

Morse code creates ambiguity. Avoid ambiguity by making code prefix free. A prefix-free code is one in which no codeword is a prefix of any other. 

Some prefix-free codes are better for some texts than others. It’d be useful to have a procedure that calculates the optimized code for a given text.

## Huffman Coding

### General Idea

Calculate relative frequencies.
* Assign each symbol to a node with weight = relative frequency.
* Take the two smallest nodes and merge them into a super node with weight equal to sum of weights.
* Repeat until everything is part of a tree.

### Data Structures

For encoding (bitstream to compressed bitstream):
* Array of BitSequence[], to retrieve, can use character as index.
* HashMap<Character, BitSequence>. (Lookup in a hashmap consists of: Compute hashCode, mod by number of buckets, look in a linked list.)

Compared to HashMaps, Arrays are faster, but use more memory if some characters in the alphabet are unused.

For decoding (compressed bitstream back to bitstream):
* Tries. (Need to look up longest matching prefix.)

### Practice

Two possible philosophies for using Huffman Compression:
1.  Build one corpus per input type.
2.  For every possible input file, create a unique code just for that file. Send the code along with the compressed file.

* Approach 1 will result in suboptimal encoding.
* Approach 2 requires to use extra space for the codeword table in the compressed bitstream. (Use in real world.)

Given a file X.txt that prepares to compress into X.huf:
* Consider each b-bit symbol of X.txt, counting occurrences of each of the 2b possibilities, where b is the size of each symbol in bits.
* Use Huffman code construction algorithm to create a decoding trie and encoding map. Store this trie at the beginning of X.huf.
* Use encoding map to write codeword for each symbol of input into X.huf.

To decompress X.huf:
* Read in the decoding trie.
* Repeatedly use the decoding trie’s `longestPrefixOf` operation until all bits in X.hug have been converted back to their uncompressed form.

## Compression Theory

The big idea in Huffman Coding is representing common symbols with small numbers of bits.

General idea: Exploit redundancy and existing order inside the sequence.

Different compression algorithms achieve different compression ratios on different files.

There's no compression algorithm that can compress any bitstream by 50%.

* Argument 1: If true, it will be able to compress any bitstream down to a single bit. Interpreter would have to be able to do the following task for any output sequence.
* Argument 2: There are far fewer short bitstreams than long ones.

### Compression Model

Universal compression is impossible, but the following example implies that comparing compression algorithms could still be quite difficult. 

Suppose there is a special purpose compression algorithm that simply hardcodes small bit sequences into large ones. Example, represent GameOfThronesSeason6-Razor1911-Rip-Episode1.mp4 as 010. To avoid this sort of trickery, the compression model should include the bits needed to encode the decompression algorithm itself. 

Compression Model #2: Self-Extracting Bits

As a model for the decompression process, the algorithm and the compressed bitstream could be treated as a single sequence of bits. (Can think of the algorithm and compressed bitstream as an input to an interpreter.)
