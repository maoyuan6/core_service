using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Respository.Global;
using Tea;

namespace Infrastructure.Helpers
{
    public class SmsHelper
    {
        /**
      * 使用AK&SK初始化账号Client
      * @param accessKeyId
      * @param accessKeySecret
      * @return Client
      * @throws Exception
      */
        public static AlibabaCloud.SDK.Dysmsapi20170525.Client CreateClient(string accessKeyId, string accessKeySecret)
        {
            AlibabaCloud.OpenApiClient.Models.Config config = new AlibabaCloud.OpenApiClient.Models.Config
            {
                // 必填，您的 AccessKey ID
                AccessKeyId = accessKeyId,
                // 必填，您的 AccessKey Secret
                AccessKeySecret = accessKeySecret,
            };
            // Endpoint 请参考 https://api.aliyun.com/product/Dysmsapi
            config.Endpoint = "dysmsapi.aliyuncs.com";
            return new AlibabaCloud.SDK.Dysmsapi20170525.Client(config);
        }

        public static void SendSms(SmsModel arg)
        {
            AlibabaCloud.SDK.Dysmsapi20170525.Client client = CreateClient(GlobalContext.SystemConfig.AlibabaConfig.AccessKeyID, GlobalContext.SystemConfig.AlibabaConfig.AccessKeySecret);
            // 1.发送短信
            AlibabaCloud.SDK.Dysmsapi20170525.Models.SendSmsRequest sendReq = new AlibabaCloud.SDK.Dysmsapi20170525.Models.SendSmsRequest
            {
                PhoneNumbers = arg.PhoneNumbers,
                SignName = arg.SignName,
                TemplateCode = arg.TemplateCode,
                TemplateParam = arg.TemplateParam,
            };
            AlibabaCloud.SDK.Dysmsapi20170525.Models.SendSmsResponse sendResp = client.SendSms(sendReq);
            string code = sendResp.Body.Code;
            if (!AlibabaCloud.TeaUtil.Common.EqualString(code, "OK"))
            {
                throw new Exception("短信发送异常，详细信息：" + sendResp.Body.Message);
                return;
            }
            string bizId = sendResp.Body.BizId;
            // 2. 等待 10 秒后查询结果
            AlibabaCloud.TeaUtil.Common.Sleep(10000);
            // 3.查询结果
            List<string> phoneNums = arg.PhoneNumbers.Split(",").ToList();

            foreach (var phoneNum in phoneNums)
            {
                AlibabaCloud.SDK.Dysmsapi20170525.Models.QuerySendDetailsRequest queryReq = new AlibabaCloud.SDK.Dysmsapi20170525.Models.QuerySendDetailsRequest
                {
                    PhoneNumber = AlibabaCloud.TeaUtil.Common.AssertAsString(phoneNum),
                    BizId = bizId,
                    SendDate =DateTime.Now.ToString("yyyyMMdd"),
                    PageSize = 10,
                    CurrentPage = 1,
                };
                AlibabaCloud.SDK.Dysmsapi20170525.Models.QuerySendDetailsResponse queryResp = client.QuerySendDetails(queryReq);
                List<AlibabaCloud.SDK.Dysmsapi20170525.Models.QuerySendDetailsResponseBody.QuerySendDetailsResponseBodySmsSendDetailDTOs.QuerySendDetailsResponseBodySmsSendDetailDTOsSmsSendDetailDTO> dtos = queryResp.Body.SmsSendDetailDTOs.SmsSendDetailDTO;
                // 打印结果

                foreach (var dto in dtos)
                {
                    if (AlibabaCloud.TeaUtil.Common.EqualString("" + dto.SendStatus, "3"))
                    {
                        Console.WriteLine("" + dto.PhoneNum + " 发送成功，接收时间: " + dto.ReceiveDate);
                    }
                    else if (AlibabaCloud.TeaUtil.Common.EqualString("" + dto.SendStatus, "2"))
                    {
                        Console.WriteLine("" + dto.PhoneNum + " 发送失败");
                    }
                    else
                    {
                        Console.WriteLine("" + dto.PhoneNum + " 正在发送中...");
                    }
                }
            }
        }
    }

    public class SmsModel
    {

        public string PhoneNumbers { get; set; }
        public string SignName { get; set; }
        public string TemplateCode { get; set; }
        public string TemplateParam { get; set; }
    }
}
