# Visualizing Overlapping Data Between Replicate Experiments

## Challenge Description

We perform experiments in the lab that isolate regions of the genome that are interesting in some way. In particular, one assay (Next Generation Capture-C) reports on evidence of physical interactions between one specific region of the genome with all other possible locations. The output of these experiments is a BED file that contains genomic intervals or coordinates. 

In order to increase our confidence in the output of the experiment and identify real signal from background noise, we run each experiment multiple times generating a BED file for each replicate. We are interested in visualizing the intervals of the genome where there is a high degree of overlap between the identified contacts within these BED files. 

Given a set of BED files, produce and visualize data that characterizes the amount of supporting evidence (number of overlapping intervals) for all locations. Bonus points will be given if the solution is able to accept the amount of overlap as input (e.g. user defines how much overlap they require: more than 1 base of overlap or even allow for negative overlap with a specified distance).

## Solution

**See the README.md file in the `scripts` directory for instuctions on executing.**

We have provided a simple, elegant, and performant solution that meets every all of the criteria identified by the challenge sponsers as desirible for an ideal solution.

By keeping the solution simple, we were able to focus on making sure the solution is complete, correct, and robust.

Although this work was done in a Hackathon format, what we are able to provide is **a complete, robust, production ready solution that can be put into use by scientist right now, because cures can't wait!**


### Data Processing

We spent many hours researching existing tools that could be used to solve this challenge. We found `bedmap`, one of the tools in the `bedops` package to be an ideal fit for this challenge. We leverage this proven and efficient tool for our data processing.

We've packaged access to this tool in some simple shell scripts for ease of use conducted extensive testing of its fitness of purpose.

The result is a production ready tool that is simple enough that "even a genomics researcher can use it".

Yes, these are brilliant scientist, but they are not necessarily expert programers so we've focused on a simple solution that can be easily used and maintained by the end user.


### Scalability

To test the scalability, we started with 25 copies of an [HG38 BED file](https://useast.ensembl.org/info/data/ftp/index.html) with 432604 lines of data in each file. Testing was performed on a Linux laptop with 8 2.7GHz cores Linux and 64GB of RAM and SSD.

Processing the data took less than 1 minute.

Process 50 replicas of the same data took less than 2-1/2 min resulting in an output file with over 21M lines of data.

| files | # comparisons | processing time
|:-----:|:-------------:|:--------------:
| 10    | 43260400      | 19s
| 25    | 10815100      | 58s
| 50    | 21630200      | 2m 25s

The performance tests can be repeated on the target machine by executing
```
./pertest <num copies>
```

The data is presorted before being parsed by the `bedmap` algorithm. This means that only the small portion of data that is being compared at the time needs to be loaded into memory.

If performance on the target machine is not adaquate, its is most likely due to file I/O. To boost perforamce, upgrade to a faster hard drive. We've included the performance measurement tools here to help you justify that high speed SSD you've been wanting!


### Deploying.

The `bedops` package is a readily available open source package. To install on Ubuntu, a simple `sudo apt install bedops` is all that is needed. The remaining data analysis is performed using the `bash` shell scripting language which is pre-installed on most *nix distributions.
