# netcore webapi template
用于netcore项目的webapi模板 可用dotnet new进行项目创建

## 如何使用
### 安装模板
* 项目根目录执行`dotnet new -i .`安装`WebAPI`模板.
* 项目根目录执行`dotnet new -i ./BLL.Template`安装`BLL`模板.
* 项目根目录执行`dotnet new -i ./DAL.Template`安装`DAL`模板.
* 项目根目录执行`dotnet new -i ./Extension.Template`安装`Extension`模板.
### 使用模板
* `dotnet new kirov-webapi -n projectname`生成`sln`和`webapi`项目.
* `dotnet new kirov-bll -n projectname`生成`BLL`层项目.
* `dotnet new kirov-dal -n projectname`生成`DAL`层项目.
* `dotnet new kirov-extension -n projectname`生成`Extension`项目.

## 为何不使用自带的项目模板
* 使用自带的webapi模板构造项目,仅仅构造出一个webapi项目.
* 实际情况我们还需要引入很多nuget包,引入自己的一些工具类等.用项目模板即可统一生成,而不需要从以前的项目copy过来.
