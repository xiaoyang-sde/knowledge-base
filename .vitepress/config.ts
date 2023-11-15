import mathjax3 from "markdown-it-mathjax3";
import { defineConfig } from "vitepress";

export default defineConfig({
  title: "Xiaoyang Liu",
  description: "",
  srcDir: "./docs",
  markdown: {
    config: (md) => {
      md.use(mathjax3);
    },
  },
  vue: {
    template: {
      compilerOptions: {
        isCustomElement: (tag) =>
          [
            "math",
            "maction",
            "maligngroup",
            "malignmark",
            "menclose",
            "merror",
            "mfenced",
            "mfrac",
            "mi",
            "mlongdiv",
            "mmultiscripts",
            "mn",
            "mo",
            "mover",
            "mpadded",
            "mphantom",
            "mroot",
            "mrow",
            "ms",
            "mscarries",
            "mscarry",
            "mscarries",
            "msgroup",
            "mstack",
            "mlongdiv",
            "msline",
            "mstack",
            "mspace",
            "msqrt",
            "msrow",
            "mstack",
            "mstack",
            "mstyle",
            "msub",
            "msup",
            "msubsup",
            "mtable",
            "mtd",
            "mtext",
            "mtr",
            "munder",
            "munderover",
            "semantics",
            "math",
            "mi",
            "mn",
            "mo",
            "ms",
            "mspace",
            "mtext",
            "menclose",
            "merror",
            "mfenced",
            "mfrac",
            "mpadded",
            "mphantom",
            "mroot",
            "mrow",
            "msqrt",
            "mstyle",
            "mmultiscripts",
            "mover",
            "mprescripts",
            "msub",
            "msubsup",
            "msup",
            "munder",
            "munderover",
            "none",
            "maligngroup",
            "malignmark",
            "mtable",
            "mtd",
            "mtr",
            "mlongdiv",
            "mscarries",
            "mscarry",
            "msgroup",
            "msline",
            "msrow",
            "mstack",
            "maction",
            "semantics",
            "annotation",
            "annotation-xml",
          ].includes(tag),
      },
    },
  },
  themeConfig: {
    algolia: {
      appId: "VI4IHQNLIL",
      apiKey: "6830e4303c886bb38ce73f508d5464c4",
      indexName: "xiaoyang-liu",
    },
    socialLinks: [
      { icon: "github", link: "https://github.com/xiaoyang-sde" },
      { icon: "linkedin", link: "https://www.linkedin.com/in/xiaoyang-sde/" },
    ],
  },
});
