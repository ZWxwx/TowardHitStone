这个文件夹里存放的是陨石相关的代码
SingletonBase.cs存放单例基类
MeteoriteCreateSystem.cs为陨石生成系统相关代码	
MeteorteObject.cs为陨石实例的部分代码

        FullSymmetry,//完全对称模式
        NumSymmetry,//数量对称模式
        FullRandom,//两边完全随机
陨石生成系统已经完成，只要生成区域对称即可

如果将陨石生成的频率设成小于偏差值，就可以实现一波生成多个的功能
只要随机出来的频率小于0，就会继续下一次生成