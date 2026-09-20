
<template>
	<div>
		<div class="app-contain">
			<el-tabs
			    v-model="orderStatus"
			    type="card"
			    class="demo-tabs"
				@tab-change="orderTabClick"
				>
				<el-tab-pane label="全部" :name="''"></el-tab-pane>
				<el-tab-pane label="未支付" name="未支付" v-if="btnAuth(`orders/未支付`,'查看')"></el-tab-pane>
				<el-tab-pane label="拼团中" name="拼团中" v-if="btnAuth(`orders/拼团中`,'查看')"></el-tab-pane>
				<el-tab-pane label="已支付" name="已支付" v-if="btnAuth(`orders/已支付`,'查看')"></el-tab-pane>
				<el-tab-pane label="已发货" name="已发货" v-if="btnAuth(`orders/已发货`,'查看')"></el-tab-pane>
				<el-tab-pane label="已完成" name="已完成" v-if="btnAuth(`orders/已完成`,'查看')"></el-tab-pane>
				<el-tab-pane label="已退款" name="已退款" v-if="btnAuth(`orders/已退款`,'查看')"></el-tab-pane>
				<el-tab-pane label="已取消" name="已取消" v-if="btnAuth(`orders/已取消`,'查看')"></el-tab-pane>
			</el-tabs>
			<div class="list_search_view">
				<el-form :model="searchQuery" class="search_form" >
					<div class="search_view">
						<div class="search_label">
							订单编号：
						</div>
						<div class="search_box">
							<el-input class="search_inp" v-model="searchQuery.orderid" placeholder="订单编号"
								clearable>
							</el-input>
						</div>
					</div>
					<div class="search_btn_view">
						<el-button class="search_btn" type="primary" @click="searchClick()" size="small">搜索</el-button>
					</div>
				</el-form>
				<br>
				<div class="btn_view">
					<el-button type="success" @click="addClick" v-if="btnAuth('orders','新增')">新增</el-button>
					<el-button type="primary" :disabled="selRows.length==1?false:true" @click="editClick" v-if=" btnAuth('orders'+(orderStatus?'/'+orderStatus:''),'修改')">修改</el-button>
					<el-button type="danger" :disabled="selRows.length?false:true" @click="delClick(null)"  v-if="btnAuth('orders'+(orderStatus?'/'+orderStatus:''),'删除')">删除</el-button>
					<el-button type="default" :disabled="selRows.length?false:true" @click="exportClick" v-if="btnAuth('orders'+(orderStatus?'/'+orderStatus:''),'导出')">导出</el-button>
					<el-button type="warning" @click="echartClick1" v-if="btnAuth('orders'+(orderStatus?'/'+orderStatus:''),'年销额')">年销额</el-button>
					<el-button type="warning" @click="echartClick2" v-if="btnAuth('orders'+(orderStatus?'/'+orderStatus:''),'月销额')">月销额</el-button>
					<el-button type="warning" @click="echartClick3" v-if="btnAuth('orders'+(orderStatus?'/'+orderStatus:''),'日销统计')">日销统计</el-button>
					<el-button type="warning" @click="echartClick4" v-if="btnAuth('orders'+(orderStatus?'/'+orderStatus:''),'品销额')">品销额</el-button>
				</div>
			</div>
			<br>
			<el-table
				v-loading="listLoading"
				border 
				:stripe='true'
				@selection-change="handleSelectionChange" 
				ref="table"
				v-if="btnAuth('orders'+(orderStatus?'/'+orderStatus:''),'查看')"
				:data="list"
				@row-click="listChange">
				<el-table-column :resizable='true' align="left" header-align="left" type="selection" width="55" />
				<el-table-column label="序号" width="70" :resizable='true' :sortable='false' align="left" header-align="left">
					<template #default="scope">{{ scope.$index + 1}}</template>
				</el-table-column>
				<el-table-column
					 :resizable='true' 
					 :sortable='false' 
					 align="left" 
					 header-align="left"
					label="订单编号">
					<template #default="scope">
						{{scope.row.orderid}}
					</template>
				</el-table-column>
				<el-table-column
					 :resizable='true' 
					 :sortable='false' 
					 align="left" 
					 header-align="left"
					label="商品表名">
					<template #default="scope">
						{{scope.row.tablename}}
					</template>
				</el-table-column>
				<el-table-column
					 :resizable='true' 
					 :sortable='false' 
					 align="left" 
					 header-align="left"
					label="商品id">
					<template #default="scope">
						{{scope.row.goodid}}
					</template>
				</el-table-column>
				<el-table-column
					 :resizable='true' 
					 :sortable='false' 
					 align="left" 
					 header-align="left"
					label="商品名称">
					<template #default="scope">
						{{scope.row.goodname}}
					</template>
				</el-table-column>
				<el-table-column label="图片" width="120" :resizable='true' :sortable='false' align="left" header-align="left">
					<template #default="scope">
						<div v-if="scope.row.picture">
							<el-image v-if="scope.row.picture.substring(0,4)=='http'" preview-teleported
								:preview-src-list="[scope.row.picture.split(',')[0]]"
								:src="scope.row.picture.split(',')[0]" style="width:100px;height:100px"></el-image>
							<el-image v-else preview-teleported
								:preview-src-list="[$config.url+scope.row.picture.split(',')[0]]"
								:src="$config.url+scope.row.picture.split(',')[0]" style="width:100px;height:100px">
							</el-image>
						</div>
						<div v-else>无图片</div>
					</template>
				</el-table-column>
				<el-table-column
					 :resizable='true' 
					 :sortable='false' 
					 align="left" 
					 header-align="left"
					label="购买数量">
					<template #default="scope">
						{{scope.row.buynumber}}
					</template>
				</el-table-column>
				<el-table-column
					 :resizable='true' 
					 :sortable='false' 
					 align="left" 
					 header-align="left"
					label="单价">
					<template #default="scope">
						{{scope.row.price}}
					</template>
				</el-table-column>
				<el-table-column
					 :resizable='true' 
					 :sortable='false' 
					 align="left" 
					 header-align="left"
					label="折扣价">
					<template #default="scope">
						{{scope.row.discountprice}}
					</template>
				</el-table-column>
				<el-table-column
					 :resizable='true' 
					 :sortable='false' 
					 align="left" 
					 header-align="left"
					label="总价">
					<template #default="scope">
						{{scope.row.total}}
					</template>
				</el-table-column>
				<el-table-column
					 :resizable='true' 
					 :sortable='false' 
					 align="left" 
					 header-align="left"
					label="折扣总价格">
					<template #default="scope">
						{{scope.row.discounttotal}}
					</template>
				</el-table-column>
				<el-table-column
					 :resizable='true' 
					 :sortable='false' 
					 align="left" 
					 header-align="left"
					:formatter="orderStatusFormatter"
					label="支付类型">
				</el-table-column>
				<el-table-column
					 :resizable='true' 
					 :sortable='false' 
					 align="left" 
					 header-align="left"
					label="订单状态">
					<template #default="scope">
						{{scope.row.status}}
					</template>
				</el-table-column>
				<el-table-column
					 :resizable='true' 
					 :sortable='false' 
					 align="left" 
					 header-align="left"
					label="备注">
					<template #default="scope">
						{{scope.row.remark}}
					</template>
				</el-table-column>
				<el-table-column
					 :resizable='true' 
					 :sortable='false' 
					 align="left" 
					 header-align="left"
					label="创建时间">
					<template #default="scope">
						{{scope.row.addtime}}
					</template>
				</el-table-column>
				<el-table-column
					 :resizable='true' 
					 :sortable='false' 
					 align="left" 
					 header-align="left"
					label="商户名称">
					<template #default="scope">
						{{scope.row.shangjiazhanghao}}
					</template>
				</el-table-column>
				<el-table-column label="下单时间" :resizable='true' :sortable='false' align="left" header-align="left">
					<template #default="scope">
						{{scope.row.addtime}}
					</template>
				</el-table-column>
				<el-table-column label="操作" width="300" :resizable='true' :sortable='false' align="left" header-align="left">
					<template #default="scope">
						<el-button v-if="btnAuth('orders'+(scope.row.status?'/'+scope.row.status:''),'物流')" type="warning" @click="logisticsClick(scope.row)">物流</el-button>
						<el-button v-if="btnAuth('orders'+(scope.row.status?'/'+scope.row.status:''),'发货')" type="warning" @click="sendGoodClick(scope.row)">发货</el-button>
						<el-button v-if="btnAuth('orders'+(scope.row.status?'/'+scope.row.status:''),'确认收货')" type="warning" @click="confirmGoodClick(scope.row)">确认收货</el-button>
					</template>
				</el-table-column>
			</el-table>
			<el-pagination 
				background
				:layout="layouts.join(',')"
				:total="total" 
				:page-size="listQuery.limit"
				prev-text="<"
				next-text=">"
				:hide-on-single-page="true"
				:style='{"width":"100%","padding":"0","margin":"20px 0 0","whiteSpace":"nowrap","color":"#333","fontWeight":"500"}'
				@size-change="sizeChange"
				@current-change="currentChange" 
				@prev-click="prevClick"
				@next-click="nextClick"  />
		</div>
		<formModel ref="formRef" @formModelChange="formModelChange"></formModel>
		<!-- 统计图弹窗 -->
		<el-dialog v-model="echartVisible" :title="'年销额'" width="70%">
			<el-tabs v-model="echartActive" class="demo-tabs" @tab-change="echartTabClick" type="card">
			    <el-tab-pane label="年销额" name="1"></el-tab-pane>
			    <el-tab-pane label="月销额" name="2"></el-tab-pane>
				<el-tab-pane label="日销统计" name="3"></el-tab-pane>
				<el-tab-pane label="品销额" name="4"></el-tab-pane>
			</el-tabs>
			<div v-if="echartActive==1" id="totalEchart1" style="width:100%;height:600px;"></div>

			<div v-if="echartActive==2" id="totalEchart2" style="width:100%;height:600px;"></div>

			<div v-if="echartActive==3" id="totalEchart3" style="width:100%;height:600px;"></div>

			<div v-if="echartActive==4" id="totalEchart4" style="width:100%;height:600px;"></div>

			<template #footer>
				<span class="formModel_btn_box">
					<el-button class="formModel_cancel" @click="echartVisible=false">取消</el-button>
				</span>
			</template>
		</el-dialog>
	</div>
</template>
<script setup>
	import axios from 'axios'
	import {
		reactive,
		ref,
		getCurrentInstance,
		nextTick,
		onMounted,
		watch,
		inject
	} from 'vue'
	import {
		useRoute,
		useRouter
	} from 'vue-router'
	import {
		ElMessageBox
	} from 'element-plus'
	const context = getCurrentInstance()?.appContext.config.globalProperties;
	import formModel from './formModel.vue'
	
	//基础信息
	const tableName = 'orders'
	const formName = '商品订单'
	const route = useRoute()
	//基础信息
	onMounted(()=>{
	})
	//列表数据
	const list = ref(null)
	const table = ref(null)
	const listQuery = ref({
		page: 1,
		limit: 20,
		sort: 'id',
		order: 'desc'
	})
	const searchQuery = ref({})
	const selRows = ref([])
	const listLoading = ref(false)
	const listChange = (row) =>{
		nextTick(()=>{
			table.value.clearSelection()
			table.value.toggleRowSelection(row)
		})
	}
	//列表
	const getList = () => {
		listLoading.value = true
		let params = JSON.parse(JSON.stringify(listQuery.value))
		params['sort'] = 'id'
		params['order'] = 'desc'
		if(orderStatus.value){
			params['status'] = orderStatus.value
		}
		if(searchQuery.value.orderid&&searchQuery.value.orderid!=''){
			params['orderid'] = '%' + searchQuery.value.orderid + '%'
		}
		context?.$http({
			url: `${tableName}/page`,
			method: 'get',
			params: params
		}).then(res => {
			listLoading.value = false
			list.value = res.data.data.list
			total.value = Number(res.data.data.total)
		})
	}
	//删
	const delClick = (id) => {
		let ids = ref([])
		if (id) {
			ids.value = [id]
		} else {
			if (selRows.value.length) {
				for (let x in selRows.value) {
					ids.value.push(selRows.value[x].id)
				}
			} else {
				return false
			}
		}
		ElMessageBox.confirm(`是否删除选中${formName}`, '提示', {
			confirmButtonText: '是',
			cancelButtonText: '否',
			type: 'warning',
		}).then(() => {
			context?.$http({
				url: `${tableName}/delete`,
				method: 'post',
				data: ids.value
			}).then(res => {
				context?.$toolUtil.message('删除成功', 'success',()=>{
					getList()
				})
			})
		})
	}
	//多选
	const handleSelectionChange = (e) => {
		selRows.value = e
	}
	//列表数据
	//分页
	const total = ref(0)
	const layouts = ref(["total","prev","pager","next","sizes","jumper"])
	const sizeChange = (size) => {
		listQuery.value.limit = size
		getList()
	}
	const currentChange = (page) => {
		listQuery.value.page = page
		getList()
	}
	const prevClick = () => {
		listQuery.value.page = listQuery.value.page - 1
		getList()
	}
	const nextClick = () => {
		listQuery.value.page = listQuery.value.page + 1
		getList()
	}
	//分页
	//权限验证
	const btnAuth = (e,a)=>{
		return context?.$toolUtil.isAuth(e,a)
	}
	//搜索
	const searchClick = () => {
		listQuery.value.page = 1
		getList()
	}
	//表单
	const formRef = ref(null)
	const formModelChange=()=>{
		searchClick()
	}
	const addClick = ()=>{
		formRef.value.init()
	}
	const editClick = ()=>{
		if(selRows.value.length){
			formRef.value.init(selRows.value[0].id,'edit')
		}
	}
	
	const infoClick = (id=null)=>{
		if(id){
			formRef.value.init(id,'info')
		}
		else if(selRows.value.length){
			formRef.value.init(selRows.value[0].id,'info')
		}
	}
	// 表单
	// 预览文件
	const preClick = (file) =>{
		if(!file){
			context?.$toolUtil.message('文件不存在','error')
		}
		window.open(context?.$config.url + file)
		// const a = document.createElement('a');
		// a.style.display = 'none';
		// a.setAttribute('target', '_blank');
		// file && a.setAttribute('download', file);
		// a.href = context?.$config.url + file;
		// document.body.appendChild(a);
		// a.click();
		// document.body.removeChild(a);
	}
	// 下载文件
	const download = (file) => {
		if(!file){
			context?.$toolUtil.message('文件不存在','error')
		}
		let arr = file.replace(new RegExp('file/', "g"), "")
		axios.get((location.href.split(context?.$config.name).length>1 ? location.href.split(context?.$config.name)[0] :'') + context?.$config.name + '/file/download?fileName=' + arr, {
			headers: {
				token: context?.$toolUtil.storageGet('Token')
			},
			responseType: "blob"
		}).then(({
			data
		}) => {
			const binaryData = [];
			binaryData.push(data);
			const objectUrl = window.URL.createObjectURL(new Blob(binaryData, {
				type: 'application/pdf;chartset=UTF-8'
			}))
			const a = document.createElement('a')
			a.href = objectUrl
			a.download = arr
			// a.click()
			// 下面这个写法兼容火狐
			a.dispatchEvent(new MouseEvent('click', {
				bubbles: true,
				cancelable: true,
				view: window
			}))
			window.URL.revokeObjectURL(data)
		})
	}
	// 统计图1
	const echarts = inject("echarts")
	const echartVisible = ref(false)
	const echartClick1 = ()=>{
		echartActive.value = '1'
		echartVisible.value = true
		nextTick(()=>{
			var totalEchart1 = echarts.init(document.getElementById("totalEchart1"),'macarons');
			context?.$http({
				url: `orders/value/addtime/total/年`,
				method: 'get'
			}).then(res=>{
				let obj = res.data.data
				let xAxis = [];
				let yAxis = [];
				let pArray = []
				for(let i=0;i<obj.length;i++){
				    xAxis.push(obj[i].addtime);
				    yAxis.push(parseFloat((obj[i].total)));
				    pArray.push({
				        value: parseFloat((obj[i].total)),
				        name: obj[i].addtime
				    })
				}
				var option = {};
                option = {
                    title: {
                        text: '年销额',
                        left: 'center'
                    },
                    tooltip: {
                      trigger: 'item',
                      formatter: '{b} : {c}'
                    },
                    xAxis: {
                        type: 'category',
                        data: xAxis,
                        axisLabel : {
                            rotate:40
                        }
                    },
                    yAxis: {
                        type: 'value'
                    },
                    series: [{
                        data: yAxis,
                        type: 'bar'
                    }]
                };
				// 使用刚指定的配置项和数据显示图表。
				totalEchart1.setOption(option);
				  //根据窗口的大小变动图表
				window.onresize = function() {
				    totalEchart1.resize();
				};
			})
		})
	}

	//统计图2
	const echartActive = ref('1')
	const echartTabClick = () =>{
		if(echartActive.value==1){
			echartClick1()
		}
		else if(echartActive.value==2){
			echartClick2()
		}
		else if(echartActive.value==3){
			echartClick3()
		}
		else if(echartActive.value==4){
			echartClick4()
		}
	}
	const echartClick2 = ()=>{
		echartActive.value = '2'
		echartVisible.value = true
		nextTick(()=>{
			var totalEchart2 = echarts.init(document.getElementById("totalEchart2"),'macarons');
			context?.$http({
				url: `orders/value/addtime/total/月`,
				method: 'get'
			}).then(res=>{
				let obj = res.data.data
				let xAxis = [];
				let yAxis = [];
				let pArray = []
				for(let i=0;i<obj.length;i++){
				    xAxis.push(obj[i].addtime);
				    yAxis.push(parseFloat((obj[i].total)));
				    pArray.push({
				        value: parseFloat((obj[i].total)),
				        name: obj[i].addtime
				    })
				}
				var option = {};
                option = {
                    title: {
                        text: '月销额',
                        left: 'center'
                    },
                    tooltip: {
                      trigger: 'item',
                      formatter: '{b} : {c}'
                    },
                    xAxis: {
                        type: 'category',
                        data: xAxis,
                        axisLabel : {
                            rotate:40
                        }
                    },
                    yAxis: {
                        type: 'value'
                    },
                    series: [{
                        data: yAxis,
                        type: 'bar'
                    }]
                };
				// 使用刚指定的配置项和数据显示图表。
				totalEchart2.setOption(option);
				  //根据窗口的大小变动图表
				window.onresize = function() {
				    totalEchart2.resize();
				};
			})
		})
	}
	//统计图3
	const echartClick3 = ()=>{
		echartActive.value = '3'
		echartVisible.value = true
		nextTick(()=>{
			var totalEchart3 = echarts.init(document.getElementById("totalEchart3"),'macarons');
			context?.$http({
				url: `orders/value/addtime/total/日`,
				method: 'get'
			}).then(res=>{
				let obj = res.data.data
				let xAxis = [];
				let yAxis = [];
				let pArray = []
				for(let i=0;i<obj.length;i++){
				    xAxis.push(obj[i].addtime);
				    yAxis.push(parseFloat((obj[i].total)));
				    pArray.push({
				        value: parseFloat((obj[i].total)),
				        name: obj[i].addtime
				    })
				}
				var option = {};
                option = {
                    title: {
                        text: '日销统计',
                        left: 'center'
                    },
                    tooltip: {
                      trigger: 'item',
                      formatter: '{b} : {c}'
                    },
                    xAxis: {
                        type: 'category',
                        data: xAxis,
                        axisLabel : {
                            rotate:40
                        }
                    },
                    yAxis: {
                        type: 'value'
                    },
                    series: [{
                        data: yAxis,
                        type: 'bar'
                    }]
                };
				// 使用刚指定的配置项和数据显示图表。
				totalEchart3.setOption(option);
				  //根据窗口的大小变动图表
				window.onresize = function() {
				    totalEchart3.resize();
				};
			})
		})
	}
	//统计图4
	const echartClick4 = ()=>{
		echartActive.value = '4'
		echartVisible.value = true
		nextTick(()=>{
			var totalEchart4 = echarts.init(document.getElementById("totalEchart4"),'macarons');
			context?.$http({
				url: `orders/value/goodname/total`,
				method: 'get'
			}).then(res=>{
				let obj = res.data.data
				let xAxis = [];
				let yAxis = [];
				let pArray = []
				for(let i=0;i<obj.length;i++){
				    xAxis.push(obj[i].goodname);
				    yAxis.push(parseFloat((obj[i].total)));
				    pArray.push({
				        value: parseFloat((obj[i].total)),
				        name: obj[i].goodname
				    })
				}
				var option = {};
                option = {
                        title: {
                            text: '品销额',
                            left: 'center'
                        },
                        legend: {
                          orient: 'vertical',
                          left: 'left'
                        },
                        tooltip: {
                          trigger: 'item',
                          formatter: '{b} : {c} ({d}%)'
                        },
                        series: [
                            {
                                type: 'pie',
                                radius: '55%',
                                center: ['50%', '60%'],
                                data: pArray,
                                emphasis: {
                                    itemStyle: {
                                        shadowBlur: 10,
                                        shadowOffsetX: 0,
                                        shadowColor: 'rgba(0, 0, 0, 0.5)'
                                    }
                                }
                            }
                        ]
                };
				// 使用刚指定的配置项和数据显示图表。
				totalEchart4.setOption(option);
				  //根据窗口的大小变动图表
				window.onresize = function() {
				    totalEchart4.resize();
				};
			})
		})
	}
	//导出选中数据
	const exportClick = () => {
		import('@/utils/Export2Excel').then(excel => {
			const tHeader = [
				"订单编号",
				"商品表名",
				"商品id",
				"商品名称",
				"图片",
				"购买数量",
				"单价",
				"折扣价",
				"总价",
				"折扣总价格",
				"支付类型",
				"订单状态",
				"地址",
				"电话",
				"收货人",
				"备注",
				"物流",
				"创建时间",
				"用户id",
				"商户名称",
			]
			const filterVal = [
				"orderid",
				"tablename",
				"goodid",
				"goodname",
				"picture",
				"buynumber",
				"price",
				"discountprice",
				"total",
				"discounttotal",
				"type",
				"status",
				"address",
				"tel",
				"consignee",
				"remark",
				"logistics",
				"addtime",
				"userid",
				"shangjiazhanghao",
			]
			excel.export_json_to_excel2(tHeader, selRows.value, filterVal, formName)
		})
	}
	const orderTabClick = () => {
		getList()
	}
	const orderStatus = ref('')
	const orderStatusFormatter =(row, column)=> {
      var temp = ''
      switch (row.type-0) {
        case 1:
          temp = '现金'
          break
        case 2:
          temp = '积分'
          break
        case 3:
          temp = '拼团'
          break
      }
      return temp
    }
	//修改物流
	const logisticsClick = (row)=>{
		formRef.value.init(row.id,'logistics','',row)
	}
	//发货
	const sendGoodClick=(row)=>{
		ElMessageBox.confirm(`确定该订单商品发货？`, '提示', {
			confirmButtonText: '是',
			cancelButtonText: '否',
			type: 'warning',
		}).then(()=>{
			row.status = '已发货'
			context?.$http({
				url: 'orders/update',
				method: 'post',
				data: row
			}).then(res=>{
				context?.$toolUtil.message('已发货','success',()=>{
					searchClick()
				})
			})
		})
	}
	//确认收货
	const confirmGoodClick=(row)=>{
		ElMessageBox.confirm(`确定该订单商品已收货？`, '提示', {
			confirmButtonText: '是',
			cancelButtonText: '否',
			type: 'warning',
		}).then(()=>{
			row.status = '已完成'
			context?.$http({
				url: 'orders/update',
				method: 'post',
				data: row
			}).then(res=>{
				context?.$toolUtil.message('订单完成','success',()=>{
					searchClick()
				})
			})
		})
	}

	//初始化
	const init = () => {
		getList()
	}
	init()
</script>
<style lang="scss" scoped>
	// 切换栏
	.demo-tabs {
		// 头部
		:deep(.el-tabs__header) {
			border: 1px solid #e4e7ed;
			padding: 0;
			box-shadow: none;
			margin: 0 0 15px;
			background: transparent;
			width: 100%;
			border-width: 0;
			line-height: auto;
			height: auto;
			// 滑动区
			.el-tabs__nav-scroll {
				padding: 0 15px;
				display: flex;
				// list
				.el-tabs__nav {
					border: 1px solid #e4e7ed;
					border-width: 0;
					// item
					.el-tabs__item {
						border: 1px solid #999;
						padding: 0 20px;
						margin: 0 4px;
						color: #333;
						border-width: 0 0 1px;
						line-height: 40px;
						height: 40px;
					}
					// item active
					.is-active {
						border: 1px solid rgba(52,136,192,1);
						padding: 0 20px;
						margin: 0 4px;
						color: rgba(52,136,192,1);
						background: transparent;
						border-color: #fff;
						border-width: 0 0 1px;
						line-height: 40px;
						height: 40px;
					}
					// item hover
					.el-tabs__item:hover {
						border: 1px solid rgba(52,136,192,1);
						padding: 0 20px;
						margin: 0 4px;
						color: rgba(64, 158, 255, 1);
						background: transparent;
						border-color: #fff;
						border-width: 0 0 1px;
						line-height: 40px;
						height: 40px;
					}
				}
			}
		}
	}
	
	// 操作盒子
	.list_search_view {
		margin: 0 0 20px;
		// 搜索盒子
		.search_form {
			display: flex;
			align-items: center;
			// 子盒子
			.search_view {
				margin: 0 10px 0 0;
				display: flex;
				align-items: center;
				// 搜索label
				.search_label {
					margin: 0 10px 0 0;
					color: #666;
					font-weight: 500;
					display: none;
					font-size: 14px;
					line-height: 40px;
					height: 40px;
				}
				// 搜索item
				.search_box {
					display: flex;
					width: calc(100% - 100px);
					// 输入框
					:deep(.search_inp) {
						border: 1px solid #ddd;
						padding: 0 10px;
						background: #fff;
						width: 100%;
						line-height: 36px;
						box-sizing: border-box;
						//去掉默认样式
						.el-input__wrapper{
							border: none;
							box-shadow: none;
							background: none;
							border-radius: 0;
							height: 100%;
							padding: 0;
						}
						.is-focus {
							box-shadow: none !important;
						}
					}
				}
			}
			// 搜索按钮盒子
			.search_btn_view {
				width: 20%;
				display: flex;
				padding: 0 20px;
				// 搜索按钮
				.search_btn {
					border: 1px solid #aaa;
					cursor: pointer;
					border-radius: 0;
					padding: 0 16px;
					outline: none;
					color: #666;
					background: #f5f5f5;
					width: auto;
					font-size: 14px;
					height: 36px;
				}
				// 搜索按钮-悬浮
				.search_btn:hover {
					box-shadow: inset 0 4px 24px 6px rgba(255,255,255,.3);
					background: #ddd;
				}
			}
		}
		//头部按钮盒子
		.btn_view {
			margin: 20px 0;
			display: flex;
			// 其他
			:deep(.el-button--default){
				border: 1px solid #ddd;
				cursor: pointer;
				border-radius: 0;
				padding: 0 16px;
				margin: 0;
				outline: none;
				color: #333;
				background: #f5f5f5;
				width: auto;
				font-size: 14px;
				height: 32px;
			}
			// 其他-悬浮
			:deep(.el-button--default:hover){
				background: rgba(85, 170, 127, 0.5);
			}
			// 新增
			:deep(.el-button--success){
				border: 1px solid #ddd;
				cursor: pointer;
				border-radius: 0;
				padding: 0 16px;
				margin: 0;
				outline: none;
				color: #333;
				background: #f5f5f5;
				width: auto;
				font-size: 14px;
				height: 32px;
			}
			// 新增-悬浮
			:deep(.el-button--success:hover){
				background: rgba(85, 255, 127, 0.5);
			}
			// 修改
			:deep(.el-button--primary){
				border: 1px solid #ddd;
				cursor: pointer;
				border-radius: 0;
				padding: 0 16px;
				margin: 0;
				outline: none;
				color: #333;
				background: #f5f5f5;
				width: auto;
				font-size: 14px;
				height: 32px;
			}
			// 修改-悬浮
			:deep(.el-button--primary:hover){
				background: rgba(64, 158, 255, 0.5);
			}
			// 详情
			:deep(.el-button--info){
				border: 1px solid #ddd;
				cursor: pointer;
				border-radius: 0;
				padding: 0 16px;
				margin: 0;
				color: #333;
				background: #f5f5f5;
				display: none;
				width: auto;
				font-size: 14px;
				height: 32px;
			}
			// 详情-悬浮
			:deep(.el-button--info:hover){
				background: rgba(85, 85, 127, 0.5);
			}
			// 删除
			:deep(.el-button--danger){
				border: 1px solid #ddd;
				cursor: pointer;
				border-radius: 0;
				padding: 0 16px;
				margin: 0;
				outline: none;
				color: #333;
				background: #f5f5f5;
				width: auto;
				font-size: 14px;
				height: 32px;
			}
			// 删除-悬浮
			:deep(.el-button--danger:hover){
				background: rgba(255, 0, 0, 0.5);
			}
			// 统计
			:deep(.el-button--warning){
				border: 1px solid #ddd;
				cursor: pointer;
				border-radius: 0;
				padding: 0 16px;
				margin: 0;
				outline: none;
				color: #333;
				background: #f5f5f5;
				width: auto;
				font-size: 14px;
				height: 32px;
			}
			// 统计-悬浮
			:deep(.el-button--warning:hover){
				background: rgba(255, 128, 0, 0.5);
			}
		}
	}
	// 表格样式
	.el-table {
		padding: 0;
		background: #fff;
		width: 100%;
		border-color: #eee;
		border-width: 1px 0 0 1px;
		border-style: solid;
		:deep(.el-table__header-wrapper) {
			thead {
				color: #999;
				font-weight: 500;
				width: 100%;
				tr {
					background: #fff;
					th {
						padding: 12px 0;
						border-color: #eee;
						border-width: 0 1px 1px 0;
						border-style: solid;
						text-align: center;
						.cell {
							padding: 0 10px;
							word-wrap: normal;
							color: #000;
							word-break: break-all;
							white-space: normal;
							font-weight: bold;
							display: inline-block;
							vertical-align: middle;
							width: 100%;
							line-height: 24px;
							position: relative;
							text-overflow: ellipsis;
						}
					}
				}
			}
		}
		:deep(.el-table__body-wrapper) {
			tbody {
				width: 100%;
				tr {
					background: #fff;
					td {
						padding: 12px 0;
						color: #666;
						background: #fff;
						border-color: #eee;
						border-width: 0 1px 1px 0;
						border-style: solid;
						text-align: center;
						.cell {
							padding: 0 10px;
							overflow: hidden;
							word-break: break-all;
							white-space: normal;
							line-height: 24px;
							text-overflow: ellipsis;
							// 编辑
							.el-button--primary {
								border: 0;
								cursor: pointer;
								border-radius: 4px;
								padding: 0 24px;
								margin: 0 10px 0 0;
								outline: none;
								color: #fff;
								background: rgba(1, 145, 92, 1);
								width: auto;
								font-size: 14px;
								height: 32px;
							}
							// 编辑-悬浮
							.el-button--primary:hover {
								background: rgba(1, 145, 92, .5);
							}
							// 详情
							.el-button--info {
								border: 0;
								cursor: pointer;
								border-radius: 4px;
								padding: 0 24px;
								margin: 0 10px 0 0;
								outline: none;
								color: #fff;
								background: rgba(7,174,247,1);
								width: auto;
								font-size: 14px;
								height: 32px;
							}
							// 详情-悬浮
							.el-button--info:hover {
								background: rgba(7,174,247,.5);
							}
							// 删除
							.el-button--danger {
								border: 0;
								cursor: pointer;
								border-radius: 4px;
								padding: 0 24px;
								margin: 0 10px 0 0;
								outline: none;
								color: #fff;
								background: rgba(239, 85, 45, 1);
								width: auto;
								font-size: 14px;
								height: 32px;
							}
							// 删除-悬浮
							.el-button--danger:hover {
								background: rgba(239, 85, 45, .5);
							}
							// 跨表
							.el-button--success {
								border: 0;
								cursor: pointer;
								border-radius: 4px;
								padding: 0 24px;
								margin: 0 10px 0 0;
								outline: none;
								color: #fff;
								background: rgba(255, 85, 255, 1.0);
								width: auto;
								font-size: 14px;
								height: 32px;
							}
							// 跨表-悬浮
							.el-button--success:hover {
								background: rgba(255, 85, 255, 0.5);
							}
							// 操作
							.el-button--warning {
								border: 0;
								cursor: pointer;
								border-radius: 4px;
								padding: 0 24px;
								margin: 0 10px 0 0;
								outline: none;
								color: #fff;
								background: rgba(85, 0, 255, 1.0);
								width: auto;
								font-size: 14px;
								height: 32px;
							}
							// 操作-悬浮
							.el-button--warning:hover {
								background: rgba(85, 0, 255, 0.5);
							}
						}
					}
				}
				tr.el-table__row--striped {
					td {
						background: #FAFAFA;
					}
				}
				tr:hover {
					td {
						padding: 12px 0;
						color: #000;
						background: rgba(123,123,123,.2);
						border-color: #eee;
						border-width: 0 1px 1px 0;
						border-style: solid;
						text-align: center;
					}
				}
			}
		}
	}
	// 分页器
	.el-pagination {
		// 总页码
		:deep(.el-pagination__total) {
			margin: 0 10px 0 0;
			color: #666;
			font-weight: 400;
			display: inline-block;
			vertical-align: top;
			font-size: 13px;
			line-height: 28px;
			height: 28px;
		}
		// 上一页
		:deep(.btn-prev) {
			border: none;
			border-radius: 2px;
			padding: 0;
			margin: 0 5px;
			color: #666;
			background: #f4f4f5;
			display: inline-block;
			vertical-align: top;
			font-size: 13px;
			line-height: 28px;
			min-width: 35px;
			height: 28px;
		}
		// 下一页
		:deep(.btn-next) {
			border: none;
			border-radius: 2px;
			padding: 0;
			margin: 0 5px;
			color: #666;
			background: #f4f4f5;
			display: inline-block;
			vertical-align: top;
			font-size: 13px;
			line-height: 28px;
			min-width: 35px;
			height: 28px;
		}
		// 上一页禁用
		:deep(.btn-prev:disabled) {
			border: none;
			cursor: not-allowed;
			border-radius: 2px;
			padding: 0;
			margin: 0 5px;
			color: #C0C4CC;
			background: #f4f4f5;
			display: inline-block;
			vertical-align: top;
			font-size: 13px;
			line-height: 28px;
			height: 28px;
		}
		// 下一页禁用
		:deep(.btn-next:disabled) {
			border: none;
			cursor: not-allowed;
			border-radius: 2px;
			padding: 0;
			margin: 0 5px;
			color: #C0C4CC;
			background: #f4f4f5;
			display: inline-block;
			vertical-align: top;
			font-size: 13px;
			line-height: 28px;
			height: 28px;
		}
		// 页码
		:deep(.el-pager) {
			padding: 0;
			margin: 0;
			display: inline-block;
			vertical-align: top;
			// 数字
			.number {
				cursor: pointer;
				padding: 0 4px;
				margin: 0 5px;
				color: #666;
				display: inline-block;
				vertical-align: top;
				font-size: 13px;
				line-height: 28px;
				border-radius: 2px;
				background: #f4f4f5;
				text-align: center;
				min-width: 30px;
				height: 28px;
			}
			// 数字悬浮
			.number:hover {
				cursor: pointer;
				padding: 0 4px;
				margin: 0 5px;
				color: #409EFF;
				display: inline-block;
				vertical-align: top;
				font-size: 13px;
				line-height: 28px;
				border-radius: 2px;
				background: rgba(52,136,192,1);
				text-align: center;
				min-width: 30px;
				height: 28px;
			}
			// 选中
			.number.is-active {
				cursor: default;
				padding: 0 4px;
				margin: 0 5px;
				color: #FFF;
				display: inline-block;
				vertical-align: top;
				font-size: 13px;
				line-height: 28px;
				border-radius: 2px;
				background: rgba(52,136,192,1);
				text-align: center;
				min-width: 30px;
				height: 28px;
			}
		}
		// sizes
		:deep(.el-pagination__sizes) {
			display: inline-block;
			vertical-align: top;
			font-size: 13px;
			line-height: 28px;
			height: 28px;
			.el-select {
				border: 1px solid #DCDFE6;
				cursor: pointer;
				padding: 0;
				color: #606266;
				display: inline-block;
				font-size: 13px;
				line-height: 28px;
				border-radius: 3px;
				outline: 0;
				background: #FFF;
				width: 100%;
				text-align: center;
				height: 28px;
			}
		}
		// 跳页
		:deep(.el-pagination__jump) {
			margin: 0 0 0 24px;
			color: #606266;
			display: inline-block;
			vertical-align: top;
			font-size: 13px;
			line-height: 28px;
			height: 28px;
			// 输入框
			.el-input {
				border: 1px solid #DCDFE6;
				cursor: pointer;
				padding: 0 3px;
				color: #606266;
				display: inline-block;
				font-size: 14px;
				line-height: 28px;
				border-radius: 3px;
				outline: 0;
				background: #FFF;
				width: 100%;
				text-align: center;
				height: 28px;
				//去掉默认样式
				.el-input__wrapper{
					border: none;
					box-shadow: none;
					background: none;
					border-radius: 0;
					height: 100%;
					padding: 0;
				}
				.is-focus {
					box-shadow: none !important;
				}
			}
		}
	}
</style>
