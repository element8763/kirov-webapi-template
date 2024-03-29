# kirov webapi template
[![NuGet](https://img.shields.io/nuget/v/kirov-webapi.svg?style=flat-square&logo=nuget)](https://www.nuget.org/packages/kirov-webapi)
![GitHub repo size in bytes](https://img.shields.io/github/repo-size/kirov-opensource/kirov-webapi-template.svg?style=flat-square&logo=github)
[![GitHub issues](https://img.shields.io/github/issues/kirov-opensource/kirov-webapi-template.svg?style=flat-square&logo=github)](https://github.com/kirov-opensource/kirov-webapi-template/issues)
![GitHub top language](https://img.shields.io/github/languages/top/kirov-opensource/kirov-webapi-template?style=flat-square&logo=github)

用于netcore项目的webapi模板 可用dotnet new进行项目创建

## 理念
在我们使用一套熟悉的框架后，创建新项目时很容易就将原有的项目复制过来改一改接着用，这种方式有好处就是你的一些工具类和拓展类可以持续的添砖加瓦，里面会有越来越多的你顺手的方法，还有些底层的东西都是相同的，你是不愿意重新再弄一套。
但是这个方式有个坏处就是很容易会复制过来很多没用的，甚至有业务关联性的代码，这是我们所不需要的，时间长了从A项目到B项目到C项目，这个框架会变得越来越不像当初的样子，而是参杂着某些业务相关性的代码或者方法。解决此问题有两个方向，一是每次都重新造轮子，从零开始构造整个架构，另一个方向则是创建一个基本的业务无关的模板。第一个方向太傻太累且每次构建的项目都可能不同，也没法持续积累你的生产力工具方法，另一个方向则可以使用CLI命令完成框架的构建，平时新增加的工具方法以及拓展类则可以同步修改此项目。因此我们创建了这个项目。

## 如何使用
### 安装模板
#### 从`nuget`安装
* `dotnet new -i kirov-webapi` 从`nuget`安装`kirov-webapi`模板.

#### 从源构建
* 项目根目录执行`dotnet new -i .`安装`kirov-webapi`模板.

### 使用模板
* `dotnet new kirov-webapi -n projectname`生成`kirov-webapi`项目.

### 卸载模板
* `dotnet new -u kirov-webapi`卸载`kirov-webapi`模板.

## 产物目录结构
```sh
.
├─Project.BLL
│  ├─Models                         #业务层模型，对外展示的模型
│  └─Services                       #服务层，每个模块对应一个Service
├─Project.DAL
│  ├─Entities                       #数据库模型映射都存放于此
│  └─Repositories                   #每个模块对应一个Repository
├─Project.Extension
│  └─Exceptions                     #通用的异常类
├─Project.WebAPI
│  ├─Controllers                    #控制器文件夹
│  ├─Extensions                     #仅限WebAPI项目使用的拓展
│  ├─Filter                         #过滤器
│  └─Model                          #仅限WebAPI使用的模型
└─migrations
    └─sql                           #Database First风格的数据库migration语句
```
