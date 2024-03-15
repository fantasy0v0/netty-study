package com.github.fantasy0v0.netty;

import com.github.fantasy0v0.netty.vo.ServiceMessage;
import io.netty.buffer.ByteBuf;
import io.netty.channel.ChannelHandlerContext;
import io.netty.handler.codec.MessageToMessageDecoder;

import java.nio.charset.StandardCharsets;
import java.util.List;

public class ServiceMessageDecoder extends MessageToMessageDecoder<ByteBuf> {

  @Override
  protected void decode(ChannelHandlerContext ctx, ByteBuf msg, List<Object> out) throws Exception {
    byte type = msg.readByte();
    System.out.println("消息类型: " + type);
    short id = msg.readShort();
    System.out.println("消息编号: " + id);
    short businessType = msg.readShort();
    System.out.println("业务类型: " + businessType);
    ServiceMessage message = new ServiceMessage();
    message.setType(type);
    message.setId(id);
    message.setBusinessType(businessType);
    message.setData(msg.retainedSlice(msg.readerIndex(), msg.readableBytes()));
    out.add(message);
  }

}
