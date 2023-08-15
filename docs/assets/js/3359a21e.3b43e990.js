"use strict";(self.webpackChunk=self.webpackChunk||[]).push([[7396],{3905:function(e,t,n){n.d(t,{Zo:function(){return u},kt:function(){return m}});var r=n(7294);function o(e,t,n){return t in e?Object.defineProperty(e,t,{value:n,enumerable:!0,configurable:!0,writable:!0}):e[t]=n,e}function a(e,t){var n=Object.keys(e);if(Object.getOwnPropertySymbols){var r=Object.getOwnPropertySymbols(e);t&&(r=r.filter((function(t){return Object.getOwnPropertyDescriptor(e,t).enumerable}))),n.push.apply(n,r)}return n}function i(e){for(var t=1;t<arguments.length;t++){var n=null!=arguments[t]?arguments[t]:{};t%2?a(Object(n),!0).forEach((function(t){o(e,t,n[t])})):Object.getOwnPropertyDescriptors?Object.defineProperties(e,Object.getOwnPropertyDescriptors(n)):a(Object(n)).forEach((function(t){Object.defineProperty(e,t,Object.getOwnPropertyDescriptor(n,t))}))}return e}function s(e,t){if(null==e)return{};var n,r,o=function(e,t){if(null==e)return{};var n,r,o={},a=Object.keys(e);for(r=0;r<a.length;r++)n=a[r],t.indexOf(n)>=0||(o[n]=e[n]);return o}(e,t);if(Object.getOwnPropertySymbols){var a=Object.getOwnPropertySymbols(e);for(r=0;r<a.length;r++)n=a[r],t.indexOf(n)>=0||Object.prototype.propertyIsEnumerable.call(e,n)&&(o[n]=e[n])}return o}var c=r.createContext({}),l=function(e){var t=r.useContext(c),n=t;return e&&(n="function"==typeof e?e(t):i(i({},t),e)),n},u=function(e){var t=l(e.components);return r.createElement(c.Provider,{value:t},e.children)},p={inlineCode:"code",wrapper:function(e){var t=e.children;return r.createElement(r.Fragment,{},t)}},d=r.forwardRef((function(e,t){var n=e.components,o=e.mdxType,a=e.originalType,c=e.parentName,u=s(e,["components","mdxType","originalType","parentName"]),d=l(n),m=o,f=d["".concat(c,".").concat(m)]||d[m]||p[m]||a;return n?r.createElement(f,i(i({ref:t},u),{},{components:n})):r.createElement(f,i({ref:t},u))}));function m(e,t){var n=arguments,o=t&&t.mdxType;if("string"==typeof e||o){var a=n.length,i=new Array(a);i[0]=d;var s={};for(var c in t)hasOwnProperty.call(t,c)&&(s[c]=t[c]);s.originalType=e,s.mdxType="string"==typeof e?e:o,i[1]=s;for(var l=2;l<a;l++)i[l]=n[l];return r.createElement.apply(null,i)}return r.createElement.apply(null,n)}d.displayName="MDXCreateElement"},1006:function(e,t,n){n.r(t),n.d(t,{assets:function(){return u},contentTitle:function(){return c},default:function(){return m},frontMatter:function(){return s},metadata:function(){return l},toc:function(){return p}});var r=n(7462),o=n(3366),a=(n(7294),n(3905)),i=["components"],s={sidebar_position:3},c="Working with Blocks",l={unversionedId:"tutorial-basics/working-with-blocks",id:"tutorial-basics/working-with-blocks",title:"Working with Blocks",description:"Scripts and macros used by Janet will be always referred to as Blocks.",source:"@site/docs/tutorial-basics/working-with-blocks.md",sourceDirName:"tutorial-basics",slug:"/tutorial-basics/working-with-blocks",permalink:"/docs/tutorial-basics/working-with-blocks",draft:!1,editUrl:"https://github.com/facebook/docusaurus/tree/main/packages/create-docusaurus/templates/shared/docs/tutorial-basics/working-with-blocks.md",tags:[],version:"current",sidebarPosition:3,frontMatter:{sidebar_position:3},sidebar:"tutorialSidebar",previous:{title:"Adding shortcut to Janet",permalink:"/docs/tutorial-basics/adding-shortcut"},next:{title:"Adding new Blocks",permalink:"/docs/tutorial-basics/adding-new-blocks"}},u={},p=[],d={toc:p};function m(e){var t=e.components,n=(0,o.Z)(e,i);return(0,a.kt)("wrapper",(0,r.Z)({},d,n,{components:t,mdxType:"MDXLayout"}),(0,a.kt)("h1",{id:"working-with-blocks"},"Working with Blocks"),(0,a.kt)("p",null,"Scripts and macros used by Janet will be always referred to as ",(0,a.kt)("strong",{parentName:"p"},(0,a.kt)("em",{parentName:"strong"},"Blocks")),".\nBlocks can be found in My Documents folder, inside ",(0,a.kt)("strong",{parentName:"p"},(0,a.kt)("em",{parentName:"strong"},"Janet Blocks"))," folder."),(0,a.kt)("p",null,"Blocks are essentialy C# files. Here's an example Block:"),(0,a.kt)("pre",null,(0,a.kt)("code",{parentName:"pre",className:"language-csharp"},'//KeyCode:KEY_T\n//Name:Test\npublic class TestMacro: IJanetBlock\n{\n    public void Execute(UIApplication uiapp)\n    {\n        TaskDialog.Show("Welcome message", "A simple test!");\n    }\n}\n\nreturn typeof(TestMacro);\n')),(0,a.kt)("p",null,"If you're just using the Block and want to simply change its name or hotkey, what interests you is the metadata contained in the comments on the top of the file:"),(0,a.kt)("pre",null,(0,a.kt)("code",{parentName:"pre",className:"language-csharp"},"//KeyCode:KEY_T\n//Name:Test\n")),(0,a.kt)("ul",null,(0,a.kt)("li",{parentName:"ul"},(0,a.kt)("inlineCode",{parentName:"li"},"KeyCode")," is the hotkey to which the macro is mapped to. Any letter on the keyboard can be used, and needs to be specified using this syntax: ",(0,a.kt)("inlineCode",{parentName:"li"},"KEY_<INSERT_LETTER>"),", e.g. ",(0,a.kt)("inlineCode",{parentName:"li"},"KEY_A"),", ",(0,a.kt)("inlineCode",{parentName:"li"},"KEY_B"),"."),(0,a.kt)("li",{parentName:"ul"},(0,a.kt)("inlineCode",{parentName:"li"},"Name")," is a human-readable name for the Block. It shows up in the Block Editor and allows you to locate the Block from the Editor.")))}m.isMDXComponent=!0}}]);